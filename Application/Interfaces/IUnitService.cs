using Domain.Entities;
using Application.DTOs;
namespace Application.Interfaces {
    public interface IUnitService : IGenericService<Unit, UnitReadDto, UnitCreateDto, UnitUpdateDto> {
        Task<UnitReadDto> GetFullByIdAsync(Guid id, bool includeTesting = false);
        Task<IEnumerable<UnitReadDto>> GetAllFullAsync(bool includeTesting = false);
        Task<IEnumerable<UnitReadDto>> GetAllByFactionAsync(string faction, bool includeTesting = false);
        Task<IEnumerable<UnitReadDto>> GetAlliesAsync(int allegianceType, bool includeTesting = false);
        Task<List<string>> GetAvailableFactionsAsync(bool includeTesting = false);
        Task<List<Unit>> GetUnitsByFactionAsync(string faction, bool includeTesting = false);
        Task<Unit> GetUnitTrackedAsync(Guid unitId);
    }
}
