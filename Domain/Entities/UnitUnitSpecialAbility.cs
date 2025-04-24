using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public class UnitUnitSpecialAbility {
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public Guid UnitSpecialAbilityId { get; set; }
        public UnitSpecialAbility UnitSpecialAbility { get; set; }
    }
}
