using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces {
    public interface IForceListService: IGenericService<ForceList, ForceListReadDto, ForceListCreateDto, ForceListUpdateDto> {
        Task<IEnumerable<ForceListReadDto>> GetAllAsync(Guid userId);
        Task<IEnumerable<ForceListDeletedReadDto>> GetAllDeletedAsync();
        Task<List<string>> GetAvailableFactionsAsync(bool includeTesting = false);
        Task<List<Unit>> GetUnitsForFactionAsync(string faction, bool includeTesting = false);
        Task<Guid> CreateForceListAsync(ForceListCreateDto dto);
        Task<Guid?> GetOwnerIdAsync(Guid forceListId);
        Task<bool> AddUnitAsync(Guid forceListId, Guid unitId, bool includeTesting = false);
        Task RemoveUnitAsync(Guid forceListId, Guid unitId);
        Task<ForceListReadDto> GetByIdAsync(Guid id);
        Task<(bool isValid, List<string> errors)> ValidateAsync(Guid forceListId);
        Task<List<AssetReadDTO>> GetAssetsForFactionAsync(string faction);
        Task AddAsset(Guid forceListId, Guid assetId);
        Task RemoveAsset(Guid forceListId, Guid assetId);
    }
}
