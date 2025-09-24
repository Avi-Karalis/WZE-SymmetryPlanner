
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities {

    [Table("Units")]
    public class Unit : BaseEntity {
        public string Faction { get; set; }
        public string UnitType { get; set; }
        public IEnumerable<string> Designation { get; set; }
        public string? DesignationTypeLimit { get; set; }
        public sbyte DesignationLimitValue { get; set; } = 0;
        public ICollection<UnitUnitSpecialAbility>? UnitUnitSpecialAbility { get; set; }
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
            if (!designation.Contains("Leader")) {
                Faction = faction;
                UnitType = unitType;
                Designation = designation;
                DPCost = dpCost;
                SPCost = spCost;
                MV = mv;
                MW = mw;
                CC = cc;
                ST = st;
                DEF = def;
                AR = ar;
                W = w;
                PW = pw;
                LD = ld;
                Base = @base;
                DesignationTypeLimit = designationTypeLimit;
                DesignationLimitValue = designationLimitValue;
                FactionAvailabilities = factionAvailabilities ?? new List<string>();
                UnitWeapon = unitWeapon ?? new List<UnitWeapon>();
                UnitUnitSpecialAbility = new List<UnitUnitSpecialAbility>();
                if (unitSpecialAbilities != null) {
                    foreach (var ability in unitSpecialAbilities) {
                        UnitUnitSpecialAbility.Add(new UnitUnitSpecialAbility {
                            Unit = this,
                            UnitSpecialAbility = ability
                        });
                    }
                }
            } else if (designation.Contains("Leader")) {
                Faction = faction;
                UnitType = unitType;
                Designation = designation;
                DPCost = dpCost;
                SPCost = spCost;
                MV = mv;
                MW = (sbyte)(mw + 1);
                CC = (sbyte)(cc +1) ;
                ST = st;
                DEF = def;
                AR = ar;
                W = w;
                PW = pw;
                LD = (sbyte)(ld+2);
                Base = @base;
                DesignationTypeLimit = designationTypeLimit;
                DesignationLimitValue = designationLimitValue;
                FactionAvailabilities = factionAvailabilities ?? new List<string>();
                UnitWeapon = unitWeapon ?? new List<UnitWeapon>();
                UnitUnitSpecialAbility = new List<UnitUnitSpecialAbility>();
                if (unitSpecialAbilities != null) {
                    foreach (var ability in unitSpecialAbilities) {
                        UnitUnitSpecialAbility.Add(new UnitUnitSpecialAbility {
                            Unit = this,
                            UnitSpecialAbility = ability
                        });
                    }
                }
            }
        }

        public void AddWeapon(IEnumerable<Weapon> weapons) {
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
            specialAbilities.ToList().ForEach(sa => UnitUnitSpecialAbility.Add(new UnitUnitSpecialAbility { Unit = this, UnitSpecialAbility = sa }));
        }

        public void AddUnitSpecialAbility(UnitSpecialAbility specialAbility) {
            UnitUnitSpecialAbility.Add(new UnitUnitSpecialAbility {
                Unit = this,
                UnitSpecialAbility = specialAbility
            });
        }
    }

}
