
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("WeaponSpecialAbilities")]
    public class WeaponSpecialAbility : BaseEntity {
        public required string Name { get; set; }
        public sbyte? NumericValue { get; set; }
        public required string Description { get; set; }
    }
}
