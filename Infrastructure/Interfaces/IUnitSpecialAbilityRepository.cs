using Domain.Entities;

namespace Infrastructure.Interfaces {
    public interface IUnitSpecialAbilityRepository : IGenericRepository<UnitSpecialAbility> {
        Task<IEnumerable<UnitSpecialAbility>> GetByNameAsync(string name);
    }
}
