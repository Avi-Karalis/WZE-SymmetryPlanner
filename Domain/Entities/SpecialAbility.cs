using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities {
    public class SpecialAbility : BaseEntity {
        public required string Name { get; set; }
        public sbyte? NumericValue { get; set; }
        public required string Description { get; set; }
    }
}
