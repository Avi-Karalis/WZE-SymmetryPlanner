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
            ValidateLeaderAndSpecialistTrooperConsumption(errors);
            ValidateSupportPoints(errors);
            ValidateDeploymentPoints(errors);
            ValidateAllyDpLimit(errors);

            return errors.Count == 0;
        }
        private bool IsAlly(Unit u) =>
            !string.Equals(u.Faction, Faction, StringComparison.OrdinalIgnoreCase) &&
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
                    if (!Allegiance.Name.Equals("Agents of Light", StringComparison.OrdinalIgnoreCase))
                        errors.Add("Seconding Allies require Agents of Light allegiance.");

                    if (Faction.Equals("Brotherhood", StringComparison.OrdinalIgnoreCase))
                        errors.Add("Brotherhood Forces may not take Seconding Allies.");
                }

                if (hasDarkCult) {
                    if (!Allegiance.Name.Equals("Servants of Darkness", StringComparison.OrdinalIgnoreCase))
                        errors.Add("Dark Cult Allies require Servants of Darkness allegiance.");

                    if (IsDarkLegionFaction(Faction))
                        errors.Add("Dark Legion Forces may not take Dark Cult Allies.");
                }

                if (hasAdvisor && IsDarkLegionFaction(Faction))
                    errors.Add("Dark Legion Forces may not take Advisor Allies.");

                if (!hasSeconding && !hasDarkCult && !hasAdvisor)
                    errors.Add($"Unit '{ally.UnitType}' has an unrecognised ally designation.");
            }
        }


        private void ValidateLeaderAndSpecialistTrooperConsumption(List<string> errors) {
            // Build a read-only trooper pool — each requirement checks against it independently
            // (a single trooper satisfies both a Leader and a Specialist of the same type)
            var trooperPool = Units
                .Where(u => HasEffectiveDesignation(u, "Trooper"))
                .GroupBy(u => u.UnitType, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.Count(), StringComparer.OrdinalIgnoreCase);

            foreach (var group in Units
                .Where(u => HasEffectiveDesignation(u, "Leader") || HasEffectiveDesignation(u, "Specialist"))
                .GroupBy(u => (u.UnitType, u.DesignationTypeLimit, u.DesignationLimitValue), (key, _) => key)
                .Distinct()) {

                var matchingUnits = Units
                    .Where(u => u.UnitType.Equals(group.UnitType, StringComparison.OrdinalIgnoreCase)
                             && (HasEffectiveDesignation(u, "Leader") || HasEffectiveDesignation(u, "Specialist")))
                    .ToList();

                if (!matchingUnits.Any()) continue;

                Unit sample = matchingUnits.First();
                int required = sample.DesignationLimitValue; // per unit, same for all in group
                string trooperType = string.IsNullOrWhiteSpace(sample.DesignationTypeLimit) ? "Any" : sample.DesignationTypeLimit;
                string designationLabel = HasEffectiveDesignation(sample, "Leader") ? "Leaders" : "Specialists";

                if (!HasEnoughTroopers(trooperPool, trooperType, required))
                    errors.Add($"{sample.UnitType} {designationLabel} require {required} {trooperType} Troopers, but not enough available.");
            }
        }

        private bool HasEnoughTroopers(Dictionary<string, int> pool, string type, int count) {
            if (type.Equals("Any", StringComparison.OrdinalIgnoreCase))
                return pool.Values.Sum() >= count;

            var types = type.Split(',').Select(t => t.Trim()).ToList();
            int totalAvailable = types.Sum(t => pool.TryGetValue(t, out int c) ? c : 0);
            return totalAvailable >= count;
        }
        private void ValidateSupportPoints(List<string> errors) {
            int spBudget = Units.Where(u => u.SPCost > 0).Sum(u => (int)u.SPCost); // SP granted by leaders
            int spUsed = Units.Where(u => u.SPCost < 0).Sum(u => -(int)u.SPCost); // SP consumed by support units
            if (spUsed > spBudget)
                errors.Add($"Support point total ({spUsed}) exceeds the allowed SP limit ({spBudget}).");
        }

        private void ValidateDeploymentPoints(List<string> errors) {
            int totalDP = Units.Sum(u => u.DPCost);
            if (totalDP > MaxDp)
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
