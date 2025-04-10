using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public class Weapon : BaseEntity {
        public string Name { get; set; }
        public List<SpecialAbility> SpecialAbility { get; set; }
        public sbyte CCMod { get; set; }
        public sbyte CCDam { get; set; }

        public sbyte ShortRange { get; set; }
        public sbyte ShortRangeMod { get; set; }
        public sbyte ShortRangeDam { get; set; }

        public sbyte LongRange { get; set; }
        public sbyte LongRangeMod { get; set; }
        public sbyte LongRangeDam { get; set; }

        public sbyte CritFail {  get; set; }
    }
}
