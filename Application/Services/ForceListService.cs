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

            var flu = new ForceListUnit {
                ForceListId = forceListId,
                UnitId = unitId
            };

            forceList.ForceListUnits.Add(flu); // add directly
            forceList.CurrentDp = (sbyte)((forceList.CurrentDp ?? 0) + unit.DPCost);
            forceList.CurrentSp = (sbyte)((forceList.CurrentSp ?? 0) + unit.SPCost);
            if (unit.SPCost > 0) forceList.MaxSp = (sbyte)(forceList.MaxSp - unit.SPCost);

            await _forceListRepository.SaveAsync();
        }

        public async Task RemoveUnitAsync(Guid forceListId, Guid unitId) {
            var forceList = await _forceListRepository.GetByIdWithUnitsAsync(forceListId);
            var flu = forceList.ForceListUnits.FirstOrDefault(f => f.UnitId == unitId);


            forceList.ForceListUnits.Remove(flu); // add directly
            forceList.CurrentDp = (sbyte)((forceList.CurrentDp ?? 0) - flu.Unit.DPCost);
            forceList.CurrentSp = (sbyte)((forceList.CurrentSp ?? 0) - flu.Unit.SPCost);
            if (flu.Unit.SPCost < 0) forceList.MaxSp = (sbyte)(forceList.MaxSp + flu.Unit.SPCost);

            await _forceListRepository.SaveAsync();
        }
        public async Task<(bool isValid, List<string> errors)> ValidateAsync(Guid forceListId) {
            var forceList = await _forceListRepository.GetByIdAsync(forceListId)
                ?? throw new Exception("Force list not found");

            bool valid = forceList.Validate(out var errors);
            return (valid, errors);
        }
    }
}
