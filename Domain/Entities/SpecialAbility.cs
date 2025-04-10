using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public class SpecialAbility : BaseEntity {
        public string Name { get; set; }
        public sbyte? NumericValue { get; set; }
        public string Description { get; set; }
    }
}
