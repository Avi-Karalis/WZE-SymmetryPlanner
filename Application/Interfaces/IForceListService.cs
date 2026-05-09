using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces {
    public interface IForceListService: IGenericService<ForceList, ForceListReadDto, ForceListCreateDto, ForceListUpdateDto> {
        Task<IEnumerable<ForceListReadDto>> GetAllAsync(Guid userId);
        Task<IEnumerable<ForceListDeletedReadDto>> GetAllDeletedAsync();
        Task<List<string>> GetAvailableFactionsAsync();
        Task<List<Unit>> GetUnitsForFactionAsync(string faction);
        Task<Guid> CreateForceListAsync(ForceListCreateDto dto);
        Task AddUnitAsync(Guid forceListId, Guid unitId);
        Task RemoveUnitAsync(Guid forceListId, Guid unitId);
        Task<ForceListReadDto> GetByIdAsync(Guid id);
        Task<(bool isValid, List<string> errors)> ValidateAsync(Guid forceListId);
        Task<List<AssetReadDTO>> GetAssetsForFactionAsync(string faction);
        Task AddAsset(Guid forceListId, Guid assetId);
        Task RemoveAsset(Guid forceListId, Guid assetId);
    }
}
