using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
namespace Application.Services {
    public class ForceListService: GenericService<ForceList, ForceListReadDto, ForceListCreateDto, ForceListUpdateDto>, IForceListService {
        private readonly IUnitRepository _unitRepository;
        private readonly IForceListRepository _forceListRepository;
        private readonly IMapper _mapper;

        public ForceListService(
            IForceListRepository forceListRepository,
            IUnitRepository unitService,
            IMapper mapper
        ) : base(forceListRepository, mapper) {
            _forceListRepository = forceListRepository;
            _unitRepository = unitService;
            _mapper = mapper;
        }

        public Task<List<string>> GetAvailableFactionsAsync()
            => _unitRepository.GetAvailableFactionsAsync();

        public Task<List<Unit>> GetUnitsForFactionAsync(string faction)
            => _unitRepository.GetUnitsByFactionAsync(faction);

        public async Task<Guid> CreateForceListAsync(ForceListCreateDto dto) {
            ForceList forceList = _mapper.Map<ForceList>(dto);

            await _forceListRepository.AddAsync(forceList);
            return forceList.Id;
        }
        public async Task AddUnitAsync(Guid forceListId, Guid unitId) {
            var forceList = await _forceListRepository.GetByIdAsync(forceListId)
            ?? throw new Exception("Force list not found");

            var unit = await _unitRepository.GetByIdAsync(unitId)
                ?? throw new Exception("Unit not found");

            forceList.Units.Add(unit);
            forceList.CurrentDp = (sbyte)((forceList.CurrentDp ?? 0) + unit.DPCost);
            forceList.CurrentSp = (sbyte)((forceList.CurrentSp ?? 0) + unit.SPCost);
            await _forceListRepository.UpdateAsync(forceList);
        }

        public async Task RemoveUnitAsync(Guid forceListId, Guid unitId) {
            var forceList = await _forceListRepository.GetByIdAsync(forceListId)
                ?? throw new Exception("Force list not found");

            var unit = forceList.Units.FirstOrDefault(u => u.Id == unitId);
            if (unit != null) {
                forceList.Units.Remove(unit);
                forceList.CurrentDp = (sbyte?)((forceList.CurrentDp ?? 0) - unit.DPCost);
                forceList.CurrentSp = (sbyte?)((forceList.CurrentSp ?? 0) - unit.SPCost);
                await _forceListRepository.UpdateAsync(forceList);
            }
        }

        public async Task<(bool isValid, List<string> errors)> ValidateAsync(Guid forceListId) {
            var forceList = await _forceListRepository.GetByIdAsync(forceListId)
                ?? throw new Exception("Force list not found");

            bool valid = forceList.Validate(out var errors);
            return (valid, errors);
        }
    }
}
