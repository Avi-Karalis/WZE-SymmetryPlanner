
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities {

    [Table("WeaponSpecialAbilities")]
    public class WeaponSpecialAbility : BaseEntity {
        public required string Name { get; set; }
        public string? ValueX { get; set; }
        public string? ValueY { get; set; }
        public required string Description { get; set; }
        [JsonIgnore]
        public ICollection<WeaponWeaponSpecialAbility>? WeaponWeaponSpecialAbility { get; set; }
    }
}
