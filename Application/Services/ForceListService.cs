using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Application.Services
{
    public class ForceListService : GenericService<ForceList, ForceListReadDto, ForceListCreateDto, ForceListUpdateDto>, IForceListService
    {
        private readonly IUnitService _unitService;
        private readonly IForceListRepository _forceListRepository;
        private readonly IMapper _mapper;
        private readonly IAssetService _assetService;
        public ForceListService(
            IForceListRepository forceListRepository,
            IUnitService unitService,
            IAssetService assetService,
            IMapper mapper
        ) : base(forceListRepository, mapper)
        {
            _forceListRepository = forceListRepository;
            _unitService = unitService;
            _assetService = assetService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ForceListReadDto>> GetAllAsync(Guid userId)
        {
            var lists = await _forceListRepository.GetAllWithUnitsAsync(userId);
            return _mapper.Map<IEnumerable<ForceListReadDto>>(lists);
        }

        public async Task<IEnumerable<ForceListDeletedReadDto>> GetAllDeletedAsync()
        {
            var lists = await _forceListRepository.GetAllDeletedAsync();
            return _mapper.Map<IEnumerable<ForceListDeletedReadDto>>(lists);
        }

        public async Task<ForceListReadDto> GetByIdAsync(Guid id)
        {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(id);
            return _mapper.Map<ForceListReadDto>(forceList);
        }
        public Task<List<string>> GetAvailableFactionsAsync(bool includeTesting = false)
            => _unitService.GetAvailableFactionsAsync(includeTesting);

        public Task<List<Unit>> GetUnitsForFactionAsync(string faction, bool includeTesting = false)
            => _unitService.GetUnitsByFactionAsync(faction, includeTesting);

        public async Task<List<AssetReadDTO>> GetAssetsForFactionAsync(string faction)
        {
            IEnumerable<AssetReadDTO> assets = await _assetService.GetAllByFactionAsync(faction);
            return assets?.ToList() ?? new List<AssetReadDTO>();
        }
        public async Task<Guid> CreateForceListAsync(ForceListCreateDto dto)
        {
            ForceList forceList = _mapper.Map<ForceList>(dto);

            await _forceListRepository.AddAsync(forceList);
            return forceList.Id;
        }
        public Task<Guid?> GetOwnerIdAsync(Guid forceListId)
            => _forceListRepository.GetOwnerIdAsync(forceListId);
        private static (sbyte usedSp, sbyte maxSp) CalculateSp(IEnumerable<ForceListUnit> units)
        {
            int maxSp = units
                .Where(u => (u.Unit?.SPCost ?? 0) > 0)
                .Sum(u => u.Unit!.SPCost);

            int usedSp = units
                .Where(u => (u.Unit?.SPCost ?? 0) < 0)
                .Sum(u => Math.Abs(u.Unit!.SPCost));

            return ((sbyte)usedSp, (sbyte)maxSp);
        }
        private static int CalculateDp(ForceList forceList)
        {
            return forceList.ForceListUnits.Sum(f => f.Unit?.DPCost ?? 0)
                + forceList.ForceListAssets.Sum(f => f.Asset?.DpCost ?? 0);
        }
        public async Task<bool> AddUnitAsync(Guid forceListId, Guid unitId, bool includeTesting = false)
        {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);
            var unit = await _unitService.GetUnitTrackedAsync(unitId);

            if (unit.Status != 0 && !(includeTesting && unit.Status == 1))
                return false;

            forceList.ForceListUnits.Add(new ForceListUnit
            {
                ForceListId = forceListId,
                UnitId = unitId,
                Unit = unit
            });

            forceList.CurrentDp = CalculateDp(forceList);

            var (usedSp, maxSp) = CalculateSp(forceList.ForceListUnits);

            forceList.CurrentSp = usedSp;

            forceList.MaxSp = maxSp;

            forceList.UpdatedAt = DateTime.UtcNow;

            await _forceListRepository.SaveAsync();
            return true;
        }

        public async Task RemoveUnitAsync(Guid forceListId, Guid unitId)
        {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);

            var flu = forceList.ForceListUnits.FirstOrDefault(f => f.UnitId == unitId);

            if (flu == null)
                return;

            forceList.ForceListUnits.Remove(flu);

            forceList.CurrentDp = CalculateDp(forceList);

            var (usedSp, maxSp) = CalculateSp(forceList.ForceListUnits);

            forceList.CurrentSp = usedSp;
            forceList.MaxSp = maxSp;

            forceList.UpdatedAt = DateTime.UtcNow;

            await _forceListRepository.SaveAsync();
        }

        public async Task AddAsset(Guid forceListId, Guid assetId)
        {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);
            var asset = await _assetService.GetEntityByIdAsync(assetId);
            forceList.ForceListAssets.Add(new ForceListAsset
            {
                ForceListId = forceListId,
                AssetId = assetId,
                Asset = asset
            });
            forceList.CurrentDp = CalculateDp(forceList);
            forceList.UpdatedAt = DateTime.UtcNow;
            await _forceListRepository.SaveAsync();
        }

        public async Task RemoveAsset(Guid forceListId, Guid assetId)
        {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);
            var fla = forceList.ForceListAssets.FirstOrDefault(f => f.AssetId == assetId);
            if (fla == null)
                return;
            forceList.ForceListAssets.Remove(fla);
            forceList.CurrentDp = CalculateDp(forceList);
            forceList.UpdatedAt = DateTime.UtcNow;
            await _forceListRepository.SaveAsync();
        }
        public async Task<(bool isValid, List<string> errors)> ValidateAsync(Guid forceListId)
        {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);

            bool valid = forceList.Validate(out var errors);
            return (valid, errors);
        }
    }
}
