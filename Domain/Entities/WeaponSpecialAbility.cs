
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("WeaponSpecialAbilities")]
    public class WeaponSpecialAbility : BaseEntity {
        public required string Name { get; set; }
        public string? ValueX { get; set; }
        public string? ValueY { get; set; }
        public required string Description { get; set; }
    }
}
