using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Domain.Entities {
    public class Unit : BaseEntity {
        public string Faction { get; set; }
        public string UnitType { get; set; }
        public List<string> Designation { get; set; }
        public List<SpecialAbility> SpecialAbility { get; set; }
        public sbyte DPCost { get; set; }
        public sbyte SPCost { get; set; }
        public sbyte MV {  get; set; }
        public sbyte MW { get; set; }
        public sbyte CC { get; set; }
        public sbyte ST { get; set; }
        public sbyte DEF { get; set; }
        public sbyte AR { get; set; }
        public sbyte W { get; set; }
        public sbyte PW { get; set; }
        public sbyte LD { get; set; }
        public List<string> FactionAvailabilities { get; set; }
        public sbyte Base { get; set; }

        public List<Weapon> Weapons { get; set; }
    }
}
