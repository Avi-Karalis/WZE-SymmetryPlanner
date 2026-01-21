namespace Domain.Entities {
    public class ForceList : BaseEntity {
        public string Name { get; set; }
        public string Faction { get; set; }
        public Allegiance Allegiance { get; set; }
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
        public int MaxDp { get; set; }
        public int MaxSp { get; set; }
        public int CurrentDp => Units.Sum(u => u.DPCost);
        public int CurrentSp => Units.Sum(u => u.SPCost);
        public Guid UserId { get; set; }
        public User User { get; set; }

        private static readonly string[] AllyDesignations ={"Advisor","Seconding","Dark Cult"};
        public bool Validate(out List<string> errors) {
            errors = [];

            // -----------------------------
            // Helpers / constants
            // -----------------------------
            bool IsAlly(Unit u) =>
                u.Designation.Any(d =>
                    AllyDesignations.Any(a =>
                        string.Equals(d, a, StringComparison.OrdinalIgnoreCase)));

            bool HasEffectiveDesignation(Unit u, string designation) {
                // Allies replace all designations with Trooper
                if (IsAlly(u)) {
                    return designation.Equals("Trooper", StringComparison.OrdinalIgnoreCase);
                }

                return u.Designation.Any(d =>
                    d.Equals(designation, StringComparison.OrdinalIgnoreCase));
            }

            bool IsDarkLegionFaction(string faction) =>
                faction.Contains("Dark Legion", StringComparison.OrdinalIgnoreCase);

            // -----------------------------
            // 1️⃣ Leader requirement
            // -----------------------------

            if (!Units.Any(u => HasEffectiveDesignation(u, "Leader"))) {
                errors.Add("Force must include at least one Leader.");
            }

            // -----------------------------
            // 2️⃣ Unique units
            // -----------------------------

            var duplicateUniques = Units
                .Where(u => HasEffectiveDesignation(u, "Unique"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);

            foreach (string dupe in duplicateUniques) {
                errors.Add($"Unique unit '{dupe}' appears more than once in the Force.");
            }

            // -----------------------------
            // 3️⃣ Allegiance ↔ Ally legality
            // -----------------------------

            foreach (Unit ally in Units.Where(IsAlly)) {
                bool hasSeconding = ally.Designation.Any(d =>
                    d.Equals("Seconding", StringComparison.OrdinalIgnoreCase));

                bool hasDarkCult = ally.Designation.Any(d =>
                    d.Equals("Dark Cult", StringComparison.OrdinalIgnoreCase));

                bool hasAdvisor = ally.Designation.Any(d =>
                    d.Equals("Advisor", StringComparison.OrdinalIgnoreCase));

                if (hasSeconding) {
                    if (!Allegiance.Equals("Agents of Light", StringComparison.OrdinalIgnoreCase))
                        errors.Add("Seconding Allies require Agents of Light allegiance.");

                    if (Faction.Equals("Brotherhood", StringComparison.OrdinalIgnoreCase))
                        errors.Add("Brotherhood Forces may not take Seconding Allies.");
                }

                if (hasDarkCult) {
                    if (!Allegiance.Equals("Servants of Darkness", StringComparison.OrdinalIgnoreCase))
                        errors.Add("Dark Cult Allies require Servants of Darkness allegiance.");

                    if (IsDarkLegionFaction(Faction))
                        errors.Add("Dark Legion Forces may not take Dark Cult Allies.");
                }

                if (hasAdvisor) {
                    if (IsDarkLegionFaction(Faction))
                        errors.Add("Dark Legion Forces may not take Advisor Allies.");
                }
            }

            // -----------------------------
            // 4️⃣ Dark Cult single-faction rule
            // -----------------------------

            var darkCultFactions = Units
                .Where(u => IsAlly(u) &&
                            u.Designation.Any(d =>
                                d.Equals("Dark Cult", StringComparison.OrdinalIgnoreCase)))
                .Select(u => u.Faction)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (darkCultFactions.Count > 1) {
                errors.Add("Dark Cult Allies must all come from a single Dark Legion faction.");
            }

            // -----------------------------
            // 5️⃣ Trooper pool (with Ally replacement)
            // -----------------------------

            var availableTroopers = Units
                .Where(u => HasEffectiveDesignation(u, "Trooper"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.Count(), StringComparer.OrdinalIgnoreCase);

            // -----------------------------
            // 6️⃣ Leader → Trooper consumption
            // -----------------------------

            foreach (var leaderGroup in Units
                .Where(u => HasEffectiveDesignation(u, "Leader"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)) {
                Unit leader = leaderGroup.First();
                int leaderCount = leaderGroup.Count();

                if (leader.DesignationLimitValue > 0 &&
                    !string.IsNullOrWhiteSpace(leader.DesignationTypeLimit)) {
                    int required = leaderCount * leader.DesignationLimitValue;
                    string trooperType = leader.DesignationTypeLimit;

                    availableTroopers.TryGetValue(trooperType, out int current);

                    if (current < required) {
                        errors.Add($"{leader.UnitType} Leaders require {required} {trooperType} Troopers, but only {current} present.");
                    } else {
                        availableTroopers[trooperType] -= required;
                    }
                }
            }

            // -----------------------------
            // 7️⃣ Specialist → Trooper consumption
            // -----------------------------

            foreach (var specialistGroup in Units
                .Where(u => HasEffectiveDesignation(u, "Specialist"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)) {
                Unit specialist = specialistGroup.First();
                int specialistCount = specialistGroup.Count();

                if (specialist.DesignationLimitValue > 0 &&
                    !string.IsNullOrWhiteSpace(specialist.DesignationTypeLimit)) {
                    int required = specialistCount * specialist.DesignationLimitValue;
                    string trooperType = specialist.DesignationTypeLimit;

                    availableTroopers.TryGetValue(trooperType, out int current);

                    if (current < required) {
                        errors.Add($"{specialist.UnitType} Specialists require {required} {trooperType} Troopers, but only {current} remaining.");
                    } else {
                        availableTroopers[trooperType] -= required;
                    }
                }
            }

            // -----------------------------
            // 8️⃣ Support Points (SP)
            // -----------------------------

            int totalSP = Units.Sum(u => u.SPCost);
            if (totalSP > 0) {
                errors.Add($"Support point total ({totalSP}) exceeds the allowed SP limit.");
            }

            // -----------------------------
            // 9️⃣ Deployment Points (DP)
            // -----------------------------

            int totalDP = Units.Sum(u => u.DPCost);
            if (totalDP > MaxDp) {
                errors.Add($"Deployment Point limit exceeded: {totalDP}/{MaxDp} DP used.");
            }

            // -----------------------------
            // 🔟 Ally DP limit (20%)
            // -----------------------------

            int allyDP = Units
                .Where(IsAlly)
                .Sum(u => u.DPCost);

            if (totalDP > 0 && allyDP > totalDP * 0.2) {
                errors.Add($"Allies consume {allyDP} DP ({(double)allyDP / totalDP:P0}), exceeding the 20% DP limit.");
            }

            return errors.Count == 0;
        }

    }
}
