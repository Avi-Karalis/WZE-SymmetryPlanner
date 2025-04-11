
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("Units")]
    public class Unit : BaseEntity {
        public required string Faction { get; set; }
        public required string UnitType { get; set; }
        public required List<string> Designation { get; set; }
        public List<UnitSpecialAbility>? UnitSpecialAbilities { get; set; }
        public required sbyte DPCost { get; set; }
        public required sbyte SPCost { get; set; }
        public required sbyte MV {  get; set; }
        public required sbyte MW { get; set; }
        public required sbyte CC { get; set; }
        public required sbyte ST { get; set; }
        public required sbyte DEF { get; set; }
        public required sbyte AR { get; set; }
        public required sbyte W { get; set; }
        public required sbyte PW { get; set; }
        public required sbyte LD { get; set; }
        public List<string>? FactionAvailabilities { get; set; }
        public required sbyte Base { get; set; }

        public required List<Weapon> Weapons { get; set; }
    }
}
