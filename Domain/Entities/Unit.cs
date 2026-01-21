
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities {

    [Table("Units")]
    public class Unit : BaseEntity {
        public string Faction { get; set; }
        public string UnitType { get; set; }
        public IEnumerable<string> Designation { get; set; }
        public string? DesignationTypeLimit { get; set; }
        public sbyte DesignationLimitValue { get; set; } = 0;
        public ICollection<UnitUnitSpecialAbility> UnitUnitSpecialAbilities { get; set; } = new List<UnitUnitSpecialAbility>();
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
        public ICollection<string>? FactionAvailabilities { get; set; }
        public sbyte Base { get; set; }
        public sbyte Status { get; set; } = 0; // 0 public, 1 testing
        public ICollection<UnitWeapon>? UnitWeapon { get; set; }

        public Unit() { }
        public Unit(
    string faction,
    string unitType,
    IEnumerable<string> designation,
    sbyte dpCost,
    sbyte spCost,
    sbyte mv,
    sbyte mw,
    sbyte cc,
    sbyte st,
    sbyte def,
    sbyte ar,
    sbyte w,
    sbyte pw,
    sbyte ld,
    sbyte @base,
    string? designationTypeLimit = null,
    sbyte designationLimitValue = 0,
    ICollection<UnitSpecialAbility>? unitSpecialAbilities = null,
    ICollection<string>? factionAvailabilities = null,
    ICollection<UnitWeapon>? unitWeapon = null
) {
            Faction = faction;
            UnitType = unitType;
            Designation = designation;
            DPCost = dpCost;
            SPCost = spCost;
            MV = mv;
            ST = st;
            DEF = def;
            AR = ar;
            W = w;
            PW = pw;
            Base = @base;
            DesignationTypeLimit = designationTypeLimit;
            DesignationLimitValue = designationLimitValue;
            FactionAvailabilities = factionAvailabilities ?? new List<string>();
            UnitWeapon = unitWeapon ?? new List<UnitWeapon>();

            if (designation.Contains("Leader")) {
                MW = (sbyte)(mw + 1);
                CC = (sbyte)(cc + 1);
                LD = (sbyte)(ld + 2);
            } else {
                MW = mw;
                CC = cc;
                LD = ld;
            }

            if (unitSpecialAbilities != null && unitSpecialAbilities.Count != 0)
                AddUnitSpecialAbility(unitSpecialAbilities);
        }


        public void AddWeapons(IEnumerable<Weapon> weapons) {
            weapons.ToList().ForEach(weapon => UnitWeapon.Add(new UnitWeapon {
                Unit = this,
                Weapon = weapon
            }));
        }

        public void AddWeapon(Weapon weapon) {
            UnitWeapon.Add(new UnitWeapon {
                Unit = this,
                Weapon = weapon
            });
        }

        public void AddUnitSpecialAbility(IEnumerable<UnitSpecialAbility> specialAbilities) {
            foreach (var sa in specialAbilities)
                UnitUnitSpecialAbilities.Add(new UnitUnitSpecialAbility { Unit = this, UnitSpecialAbility = sa });
        }

        public void AddUnitSpecialAbility(UnitSpecialAbility specialAbility) {
            UnitUnitSpecialAbilities.Add(new UnitUnitSpecialAbility { Unit = this, UnitSpecialAbility = specialAbility });
        }
    }

}
