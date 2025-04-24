
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {
    [Table("UnitUnitSpecialAbilities")]
    public class UnitUnitSpecialAbility {
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public Guid UnitSpecialAbilityId { get; set; }
        public UnitSpecialAbility UnitSpecialAbility { get; set; }
    }
}
