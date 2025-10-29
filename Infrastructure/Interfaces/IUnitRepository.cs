using Domain.Entities;


namespace Infrastructure.Interfaces {
    public interface IUnitRepository : IGenericRepository<Unit> {
        Task<IEnumerable<Unit>> GetByFactionAsync(string faction);
        Task<IEnumerable<Unit>> GetByDesignationAsync(string designation);
    }
}
