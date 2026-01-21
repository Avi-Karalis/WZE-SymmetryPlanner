
using System.Text.Json.Serialization;

namespace Domain.Entities {
    public class UnitWeapon {
        public Guid UnitId { get; set; }
        [JsonIgnore]
        public Unit Unit { get; set; }

        public Guid WeaponId { get; set; }
        public Weapon Weapon { get; set; }
    }
}
