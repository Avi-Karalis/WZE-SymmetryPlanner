
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Domain.Entities {
    [Table("WeaponWeaponSpecialAbilities")]
    public class WeaponWeaponSpecialAbility {
        public Guid WeaponId { get; set; }
        [JsonIgnore]
        public Weapon Weapon { get; set; }

        public Guid WeaponSpecialAbilityId { get; set; }
        public WeaponSpecialAbility WeaponSpecialAbility { get; set; }
    }
}
