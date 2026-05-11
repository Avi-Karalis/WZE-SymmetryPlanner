using Domain.Entities;

namespace Infrastructure.Interfaces {
    public interface IUnitRepository : IGenericRepository<Unit> {
        Task<Unit> GetFullByIdAsync(Guid id);
        Task<IEnumerable<Unit>> GetAllFullAsync();
        Task<IEnumerable<Unit>> GetAllByFactionAsync(string faction);
        Task<IEnumerable<Unit>> GetAlliesAsync(int allegianceType);
        Task<List<string>> GetAvailableFactionsAsync();
        Task<List<Unit>> GetUnitsByFactionAsync(string faction);
        Task<Unit> GetUnitTrackedAsync(Guid unitId);
    }
}
