using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("Weapons")]
    public class Weapon : BaseEntity {
        public required string Name { get; set; }
        public required IEnumerable<WeaponSpecialAbility> WeaponSpecialAbilities { get; set; }
        public required sbyte CCMod { get; set; }
        public required sbyte CCDam { get; set; }

        public required sbyte ShortRange { get; set; }
        public required sbyte ShortRangeMod { get; set; }
        public required sbyte ShortRangeDam { get; set; }

        public required sbyte LongRange { get; set; }
        public required sbyte LongRangeMod { get; set; }
        public required sbyte LongRangeDam { get; set; }

        public sbyte CritFail {  get; set; } = 20;

        public IEnumerable<UnitWeapon> UnitWeapon { get; set; }
    }
}
