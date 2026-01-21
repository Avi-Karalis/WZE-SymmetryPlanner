using Domain.Entities;


namespace Infrastructure.Interfaces {
    public interface IUnitRepository : IGenericRepository<Unit> {
        Task<Unit> GetFullByIdAsync(Guid id);
        Task<IEnumerable<Unit>> GetAllFullAsync();
    }
}
