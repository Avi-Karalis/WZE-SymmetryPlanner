using System.Linq;

namespace Domain.Entities {
    public class ForceList : BaseEntity {
        public string Name { get; set; }
        public string Faction { get; set; }
        public Allegiance Allegiance { get; set; }
        public sbyte MaxDp { get; set; }
        public sbyte MaxSp { get; set; }
        public sbyte? CurrentDp { get; set; }
        public sbyte? CurrentSp { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<ForceListUnit> ForceListUnits { get; set; } = new List<ForceListUnit>();
        private static readonly string[] AllyDesignations ={"Advisor","Seconding","Dark Cult"};

        private IEnumerable<Unit> Units =>
    ForceListUnits.Select(flu => flu.Unit);
        public bool Validate(out List<string> errors) {
            errors = new List<string>();

            ValidateLeaderRequirement(errors);
            ValidateUniqueUnits(errors);
            ValidateAllyLegality(errors);
            ValidateDarkCultSingleFaction(errors);
            ValidateLeaderAndSpecialistTrooperConsumption(errors);
            ValidateSupportPoints(errors);
            ValidateDeploymentPoints(errors);
            ValidateAllyDpLimit(errors);

            return errors.Count == 0;
        }
        private bool IsAlly(Unit u) =>
    u.Designation.Any(d => AllyDesignations.Any(a => string.Equals(d, a, StringComparison.OrdinalIgnoreCase)));

        private bool HasEffectiveDesignation(Unit u, string designation) {
            if (IsAlly(u)) return designation.Contains("Trooper", StringComparison.OrdinalIgnoreCase);
            return u.Designation.Any(d => d.Equals(designation, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsDarkLegionFaction(string faction) =>
            faction.Contains("Dark Legion", StringComparison.OrdinalIgnoreCase);

        private void ValidateLeaderRequirement(List<string> errors) {
            if (!Units.Any(u => HasEffectiveDesignation(u, "Leader")))
                errors.Add("Force must include at least one Leader.");
        }

        private void ValidateUniqueUnits(List<string> errors) {
            var duplicateUniques = Units
                .Where(u => HasEffectiveDesignation(u, "Unique"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);

            foreach (string dupe in duplicateUniques)
                errors.Add($"Unique unit '{dupe}' appears more than once in the Force.");
        }

        private void ValidateAllyLegality(List<string> errors) {
            foreach (Unit ally in Units.Where(IsAlly)) {
                bool hasSeconding = ally.Designation.Any(d => d.Equals("Seconding", StringComparison.OrdinalIgnoreCase));
                bool hasDarkCult = ally.Designation.Any(d => d.Equals("Dark Cult", StringComparison.OrdinalIgnoreCase));
                bool hasAdvisor = ally.Designation.Any(d => d.Equals("Advisor", StringComparison.OrdinalIgnoreCase));

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

                if (hasAdvisor && IsDarkLegionFaction(Faction))
                    errors.Add("Dark Legion Forces may not take Advisor Allies.");
            }
        }

        private void ValidateDarkCultSingleFaction(List<string> errors) {
            var darkCultFactions = Units
                .Where(u => IsAlly(u) && u.Designation.Any(d => d.Equals("Dark Cult", StringComparison.OrdinalIgnoreCase)))
                .Select(u => u.Faction)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (darkCultFactions.Count > 1)
                errors.Add("Dark Cult Allies must all come from a single Dark Legion faction.");
        }

        private void ValidateLeaderAndSpecialistTrooperConsumption(List<string> errors) {
            // Build a shared trooper pool (Troopers + Allies-as-Trooper)
            var availableTroopers = Units
                .Where(u => HasEffectiveDesignation(u, "Trooper"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.Count(), StringComparer.OrdinalIgnoreCase);

            // -----------------------------
            // Leaders first
            // -----------------------------
            foreach (var leaderGroup in Units
                .Where(u => HasEffectiveDesignation(u, "Leader"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)) {
                Unit leader = leaderGroup.First();
                int required = leaderGroup.Count() * leader.DesignationLimitValue;
                string trooperType = string.IsNullOrWhiteSpace(leader.DesignationTypeLimit) ? "Any" : leader.DesignationTypeLimit;

                if (!ConsumeTroopers(availableTroopers, trooperType, required))
                    errors.Add($"{leader.UnitType} Leaders require {required} {trooperType} Troopers, but not enough available.");
            }

            // -----------------------------
            // Specialists next
            // -----------------------------
            foreach (var specialistGroup in Units
                .Where(u => HasEffectiveDesignation(u, "Specialist"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)) {
                Unit specialist = specialistGroup.First();
                int required = specialistGroup.Count() * specialist.DesignationLimitValue;
                string trooperType = string.IsNullOrWhiteSpace(specialist.DesignationTypeLimit) ? "Any" : specialist.DesignationTypeLimit;

                if (!ConsumeTroopers(availableTroopers, trooperType, required))
                    errors.Add($"{specialist.UnitType} Specialists require {required} {trooperType} Troopers, but not enough available.");
            }
        }


        private bool ConsumeTroopers(Dictionary<string, int> pool, string type, int count) {
            if (type.Equals("Any", StringComparison.OrdinalIgnoreCase)) {
                int totalAvailable = pool.Values.Sum();
                if (totalAvailable < count) return false;

                foreach (var key in pool.Keys.ToList()) {
                    if (count <= 0) break;
                    int deduct = Math.Min(count, pool[key]);
                    pool[key] -= deduct;
                    count -= deduct;
                }

                return true;
            } else {
                pool.TryGetValue(type, out int current);
                if (current < count) return false;
                pool[type] -= count;
                return true;
            }
        }
        private void ValidateSupportPoints(List<string> errors) {
            int totalSP = Units.Sum(u => u.SPCost);
            if (totalSP <= MaxSp)
                errors.Add($"Support point total ({totalSP}) exceeds the allowed SP limit.");
        }

        private void ValidateDeploymentPoints(List<string> errors) {
            int totalDP = Units.Sum(u => u.DPCost);
            if (totalDP >= MaxDp)
                errors.Add($"Deployment Point limit exceeded: {totalDP}/{MaxDp} DP used.");
        }

        private void ValidateAllyDpLimit(List<string> errors) {
            int totalDP = Units.Sum(u => u.DPCost);
            int allyDP = Units.Where(IsAlly).Sum(u => u.DPCost);

            if (totalDP > 0 && allyDP > totalDP * 0.2)
                errors.Add($"Allies consume {allyDP} DP ({(double)allyDP / totalDP:P0}), exceeding the 20% DP limit.");
        }
    }
}
