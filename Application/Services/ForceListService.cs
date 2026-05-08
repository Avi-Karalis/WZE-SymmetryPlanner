using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Application.Services {
    public class ForceListService: GenericService<ForceList, ForceListReadDto, ForceListCreateDto, ForceListUpdateDto>, IForceListService {
        private readonly IUnitService _unitService;
        private readonly IForceListRepository _forceListRepository;
        private readonly IMapper _mapper;

        public ForceListService(
            IForceListRepository forceListRepository,
            IUnitService unitService,
            IMapper mapper
        ) : base(forceListRepository, mapper) {
            _forceListRepository = forceListRepository;
            _unitService = unitService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ForceListReadDto>> GetAllAsync() {
            var lists = await _forceListRepository.GetAllWithUnitsAsync();
            return _mapper.Map<IEnumerable<ForceListReadDto>>(lists);
        }

        public async Task<ForceListReadDto> GetByIdAsync(Guid id) {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(id);
            return _mapper.Map<ForceListReadDto>(forceList);
        }
        public Task<List<string>> GetAvailableFactionsAsync()
            => _unitService.GetAvailableFactionsAsync();

        public Task<List<Unit>> GetUnitsForFactionAsync(string faction)
            => _unitService.GetUnitsByFactionAsync(faction);

        public async Task<Guid> CreateForceListAsync(ForceListCreateDto dto) {
            ForceList forceList = _mapper.Map<ForceList>(dto);

            await _forceListRepository.AddAsync(forceList);
            return forceList.Id;
        }
        public async Task AddUnitAsync(Guid forceListId, Guid unitId) {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);
            var unit = await _unitService.GetEntityByIdAsync(unitId);

            forceList.ForceListUnits.Add(new ForceListUnit {
                ForceListId = forceListId,
                UnitId = unitId
            });

            forceList.CurrentDp = (sbyte)(forceList.ForceListUnits.Sum(f => f.Unit?.DPCost ?? 0) + unit.DPCost);
            forceList.CurrentSp = (sbyte)(forceList.ForceListUnits.Where(f => f.Unit?.SPCost > 0).Sum(f => f.Unit.SPCost) + (unit.SPCost > 0 ? unit.SPCost : 0));
            if (unit.SPCost < 0) forceList.MaxSp = (sbyte)(forceList.MaxSp + Math.Abs(unit.SPCost)); // leader grants SP budget
            forceList.UpdatedAt = DateTime.UtcNow;

            await _forceListRepository.SaveAsync();
        }

        public async Task RemoveUnitAsync(Guid forceListId, Guid unitId) {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);
            var flu = forceList.ForceListUnits.FirstOrDefault(f => f.UnitId == unitId);

            forceList.ForceListUnits.Remove(flu);

            forceList.CurrentDp = (sbyte)forceList.ForceListUnits.Sum(f => f.Unit?.DPCost ?? 0);
            forceList.CurrentSp = (sbyte)forceList.ForceListUnits.Where(f => f.Unit?.SPCost > 0).Sum(f => f.Unit.SPCost);
            if (flu.Unit.SPCost < 0) forceList.MaxSp = (sbyte)(forceList.MaxSp - Math.Abs(flu.Unit.SPCost)); // remove SP budget granted by leader
            forceList.UpdatedAt = DateTime.UtcNow;

            await _forceListRepository.SaveAsync();
        }
        public async Task<(bool isValid, List<string> errors)> ValidateAsync(Guid forceListId) {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);

            bool valid = forceList.Validate(out var errors);
            return (valid, errors);
        }
    }
}
