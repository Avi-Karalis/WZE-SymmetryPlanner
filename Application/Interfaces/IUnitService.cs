using Domain.Entities;
using Application.DTOs;
namespace Application.Interfaces {
    public interface IUnitService : IGenericService<Unit, UnitReadDto, UnitCreateDto, UnitUpdateDto> {
        Task<UnitReadDto> GetFullByIdAsync(Guid id);
        Task<IEnumerable<UnitReadDto>> GetAllFullAsync();
        Task<IEnumerable<UnitReadDto>> GetAllByFactionAsync(string faction);
        Task<List<string>> GetAvailableFactionsAsync();
        Task<List<Unit>> GetUnitsByFactionAsync(string faction);
        Task<Unit> GetUnitTrackedAsync(Guid unitId);
    }
}
