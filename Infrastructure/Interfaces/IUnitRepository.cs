using Domain.Entities;

namespace Infrastructure.Interfaces {
    public interface IUnitRepository : IGenericRepository<Unit> {
        Task<Unit> GetFullByIdAsync(Guid id, bool includeTesting = false);
        Task<IEnumerable<Unit>> GetAllFullAsync(bool includeTesting = false);
        Task<IEnumerable<Unit>> GetAllByFactionAsync(string faction, bool includeTesting = false);
        Task<IEnumerable<Unit>> GetAlliesAsync(int allegianceType, bool includeTesting = false);
        Task<List<string>> GetAvailableFactionsAsync(bool includeTesting = false);
        Task<List<Unit>> GetUnitsByFactionAsync(string faction, bool includeTesting = false);
        Task<Unit> GetUnitTrackedAsync(Guid unitId);
    }
}
