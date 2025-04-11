
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("UnitSpecialAbilities")]
    public class UnitSpecialAbility : BaseEntity {
        public required string Name { get; set; }
        public sbyte? NumericValue { get; set; }
        public required string Description { get; set; }
    }
}
