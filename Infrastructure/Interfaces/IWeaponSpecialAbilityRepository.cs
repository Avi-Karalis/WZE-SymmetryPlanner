using Domain.Entities;

namespace Infrastructure.Interfaces {
    public interface IWeaponSpecialAbilityRepository : IGenericRepository<WeaponSpecialAbility> {
        Task<IEnumerable<WeaponSpecialAbility>> GetByNameAsync(string name);
    }
}
