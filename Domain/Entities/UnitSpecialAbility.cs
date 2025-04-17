
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("UnitSpecialAbilities")]
    public class UnitSpecialAbility : BaseEntity {
        public required string Name { get; set; }
        public string? ValueX { get; set; }
        public string? ValueY { get; set; }
        public required string Description { get; set; }
        public ICollection<UnitUnitSpecialAbility>? UnitUnitSpecialAbility { get; set; }
    }
}
