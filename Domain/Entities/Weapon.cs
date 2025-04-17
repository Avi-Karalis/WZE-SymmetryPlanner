using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("Weapons")]
    public class Weapon : BaseEntity {
        public required string Name { get; set; }
        public required ICollection<WeaponWeaponSpecialAbility> WeaponWeaponSpecialAbility { get; set; }
        public sbyte? CCMod { get; set; } 
        public sbyte? CCDam { get; set; } 

        public sbyte? ShortRange { get; set; } 
        public sbyte? ShortRangeMod { get; set; }
        public sbyte? ShortRangeDam { get; set; }

        public sbyte? LongRange { get; set; }
        public sbyte? LongRangeMod { get; set; }
        public sbyte? LongRangeDam { get; set; }

        public sbyte CCDamageMultiplier { get; set; } = 1;
        public sbyte SRDamageMultiplier { get; set; } = 1;
        public sbyte LRDamageMultiplier { get; set; } = 1;
        public sbyte CritFail {  get; set; } = 20;
        public bool DynamicDAM {  get; set; } = false;

        public ICollection<UnitWeapon> UnitWeapon { get; set; } = new List<UnitWeapon>();

    }
}
