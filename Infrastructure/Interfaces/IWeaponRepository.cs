using Domain.Entities;


namespace Infrastructure.Interfaces {
    public interface IWeaponRepository : IGenericRepository<Weapon> {
        Task<Weapon> GetFullByIdAsync(Guid id);
        Task<IEnumerable<Weapon>> GetAllFullAsync();
    }
}
