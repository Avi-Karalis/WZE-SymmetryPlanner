
namespace Domain.Entities {
    public class ForceList : BaseEntity {
        public string Name { get; set; }
        public string Faction { get; set; }
        public string Allegiance { get; set; }
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
        public int MaxDp { get; set; }
        public int MaxSp { get; set; }
        public int CurrentDp => Units.Sum(u => u.DPCost);
        public int CurrentSp => Units.Sum(u => u.SPCost);


        public bool Validate(out List<string> errors) {
            errors = new List<string>();

            if (!Units.Any(u => u.Designation.Any(d => d.Equals("Leader", StringComparison.OrdinalIgnoreCase)))) {
                errors.Add("Force must include at least one Leader.");
            }
           foreach(Unit leader in Units.Where(u=> u.Designation.Any(d => d.Equals("Leader", StringComparison.OrdinalIgnoreCase)))) {
                if (!string.IsNullOrWhiteSpace(leader.DesignationTypeLimit) && leader.DesignationLimitValue > 0) {
                    int trooperCount = Units.Count(u => 
                        u.Designation.Contains("Trooper", StringComparer.OrdinalIgnoreCase) && u.UnitType.Equals(
                        leader.DesignationTypeLimit, StringComparison.OrdinalIgnoreCase));

                    if (trooperCount < leader.DesignationLimitValue) {
                        errors.Add($"{leader.UnitType} Leader requires {leader.DesignationLimitValue} {leader.DesignationTypeLimit} Troopers, but only {trooperCount} present.");
                    }
                }
                           }

            foreach (Unit specialist in Units.Where(u => u.Designation.Contains("Specialist", StringComparer.OrdinalIgnoreCase))) {
                if (!string.IsNullOrWhiteSpace(specialist.DesignationTypeLimit) && specialist.DesignationLimitValue > 0) {
                    int trooperCount = Units.Count(u =>
                        u.Designation.Contains("Trooper", StringComparer.OrdinalIgnoreCase) &&
                        u.UnitType.Equals(specialist.DesignationTypeLimit, StringComparison.OrdinalIgnoreCase));

                    if (trooperCount < specialist.DesignationLimitValue) {
                        errors.Add($"{specialist.UnitType} Specialist requires {specialist.DesignationLimitValue} {specialist.DesignationTypeLimit} Troopers, but only {trooperCount} present.");
                    }
                }
            }

            // only one of each Unique unit
            List<string> duplicateUniques = Units
                .Where(u => u.Designation.Contains("Unique", StringComparer.OrdinalIgnoreCase))
                .GroupBy(u => u.UnitType)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            foreach (string dupe in duplicateUniques) {
                errors.Add($"Unique unit '{dupe}' appears more than once in the Force.");
            }

            // 5️⃣ Support Points (SP) should not exceed limit (example: -10 SP cap)
            int totalSP = Units.Sum(u => u.SPCost);
            if (totalSP > 0) {
                errors.Add($"Support point total ({totalSP}) exceeds the allowed SP limit (should not be positive).");
            }

            // 6️⃣ Ally rule — up to 20% of DP may be spent on Allies
            int totalDP = Units.Sum(u => u.DPCost);
            int allyDP = Units
                .Where(u => u.Designation.Contains("Ally", StringComparer.OrdinalIgnoreCase))
                .Sum(u => u.DPCost);

            if (totalDP > 0 && allyDP > totalDP * 0.2) {
                errors.Add($"Allies consume {allyDP} DP ({(double)allyDP / totalDP:P0}), exceeding the 20% DP limit.");
            }

            return !errors.Any();
        }
    }
}
