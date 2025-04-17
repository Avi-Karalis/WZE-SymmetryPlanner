using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public class WeaponWeaponSpecialAbility {
        public int WeaponId { get; set; }
        public Weapon Weapon { get; set; }

        public int WeaponSpecialAbilityId { get; set; }
        public WeaponSpecialAbility WeaponSpecialAbility { get; set; }
    }
}
