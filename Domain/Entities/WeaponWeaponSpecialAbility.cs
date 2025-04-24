
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities {
    [Table("WeaponWeaponSpecialAbilities")]
    public class WeaponWeaponSpecialAbility {
        public Guid WeaponId { get; set; }
        public Weapon Weapon { get; set; }

        public Guid WeaponSpecialAbilityId { get; set; }
        public WeaponSpecialAbility WeaponSpecialAbility { get; set; }
    }
}
