
namespace Domain.Entities {
    public class UnitWeapon {
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public Guid WeaponId { get; set; }
        public Weapon Weapon { get; set; }
    }
}
