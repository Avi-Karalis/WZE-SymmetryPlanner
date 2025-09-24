using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public class WeaponWeaponSpecialAbility {
        public Guid WeaponId { get; set; }
        public Weapon Weapon { get; set; }

        public Guid WeaponSpecialAbilityId { get; set; }
        public WeaponSpecialAbility WeaponSpecialAbility { get; set; }
    }
}
