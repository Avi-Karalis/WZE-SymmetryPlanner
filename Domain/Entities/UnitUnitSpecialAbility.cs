
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities {
    [Table("UnitUnitSpecialAbilities")]
    public class UnitUnitSpecialAbility {
        public Guid UnitId { get; set; }
        [JsonIgnore]
        public Unit Unit { get; set; }

        public Guid UnitSpecialAbilityId { get; set; }
        public UnitSpecialAbility UnitSpecialAbility { get; set; }
    }
}
