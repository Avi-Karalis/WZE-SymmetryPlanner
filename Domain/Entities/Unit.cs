
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("Units")]
    public class Unit : BaseEntity {
        public required string Faction { get; set; }
        public required string UnitType { get; set; }
        public required IEnumerable<string> Designation { get; set; }
        public string? DesignationTypeLimit { get; set; }
        public sbyte DesignationLimitValue { get; set; } = 0
        public ICollection<UnitSpecialAbility>? UnitSpecialAbilities { get; set; }
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
        public ICollection<string>? FactionAvailabilities { get; set; }
        public required sbyte Base { get; set; }

        public ICollection<UnitWeapon> UnitWeapon { get; set; } = new List<UnitWeapon>();

    }
}
