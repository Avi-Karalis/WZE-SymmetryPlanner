using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;


namespace Application.Services {
    public class UnitService : GenericService<Unit, UnitReadDto, UnitCreateDto, UnitUpdateDto>, Interfaces.IUnitService {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitSpecialAbilityService _unitAbilityService;
        private readonly IWeaponService _weaponService;
        private readonly IMapper _mapper;
        public UnitService(
            IUnitRepository unitRepository,
            IUnitSpecialAbilityService unitAbilityService,
            IWeaponService weaponService,
            IMapper mapper
        ) : base(unitRepository, mapper) {
            _unitRepository = unitRepository;
            _unitAbilityService = unitAbilityService;
            _weaponService = weaponService;
            _mapper = mapper;
        }
        public async Task<UnitReadDto> GetFullByIdAsync(Guid id) {
            return _mapper.Map<UnitReadDto>(await _unitRepository.GetFullByIdAsync(id));
        }
        public async Task<IEnumerable<UnitReadDto>> GetAllByFactionAsync(string faction) { 
           return _mapper.Map<IEnumerable<UnitReadDto>>(await _unitRepository.GetAllByFactionAsync(faction));
        }
        public async Task<IEnumerable<UnitReadDto>> GetAllFullAsync() {
            return _mapper.Map<IEnumerable<UnitReadDto>>(await _unitRepository.GetAllFullAsync());
        }

        public override async Task<UnitReadDto> CreateAsync(UnitCreateDto unitDto) {
            Unit unitEntity = _mapper.Map<Unit>(unitDto);
            var newAbilities = new List<UnitSpecialAbility>();
            foreach (Guid abilityEntry in unitDto.UnitSpecialAbilityIds) {
                UnitSpecialAbility ability;

                if (abilityEntry != Guid.Empty) {
                    ability = _mapper.Map<UnitSpecialAbility>( await _unitAbilityService.GetByIdAsync(abilityEntry))
                              ?? throw new Exception("UnitSpecialAbility not found");
                } else {
                    ability = null!;
                }

                newAbilities.Add(ability);
            }

            unitEntity.AddUnitSpecialAbility(newAbilities);


            foreach (var weaponEntry in unitDto.WeaponIds ) {
                WeaponReadDto weapon;

                if (weaponEntry != Guid.Empty) {
                    weapon = _mapper.Map<WeaponReadDto>(await _weaponService.GetByIdAsync(weaponEntry))
                             ?? throw new Exception("Weapon not found");
                } else {
                    weapon = await _weaponService.CreateAsync(_mapper.Map<WeaponCreateDto>(weaponEntry));
                }

                unitEntity.AddWeapon(_mapper.Map<Weapon>(weapon));
            }


            var createdUnit = await _unitRepository.CreateAsync(unitEntity);


            return _mapper.Map<UnitReadDto>(createdUnit);
        }


        public async Task<List<string>> GetAvailableFactionsAsync() {
            return await _unitRepository.GetAvailableFactionsAsync();
        }
        public async Task<List<Unit>> GetUnitsByFactionAsync(string faction) {
            return await _unitRepository.GetUnitsByFactionAsync(faction);
        }

        public async Task<Unit> GetUnitTrackedAsync(Guid unitId) => await _unitRepository.GetUnitTrackedAsync(unitId);

        public override async Task<UnitReadDto> UpdateAsync(Guid id, UnitUpdateDto dto) {
            // Fetch tracked unit with all relationships loaded
            var unit = await _unitRepository.GetFullByIdAsync(id);

            // Update scalar properties
            unit.Faction = dto.Faction;
            unit.UnitType = dto.UnitType;
            unit.Designation = dto.Designation;
            unit.DesignationTypeLimit = dto.DesignationTypeLimit;
            unit.DesignationLimitValue = dto.DesignationLimitValue;
            unit.DPCost = dto.DPCost;
            unit.SPCost = dto.SPCost;
            unit.MV = dto.MV;
            unit.MW = dto.MW;
            unit.CC = dto.CC;
            unit.ST = dto.ST;
            unit.DEF = dto.DEF;
            unit.AR = dto.AR;
            unit.W = dto.W;
            unit.PW = dto.PW;
            unit.LD = dto.LD;
            unit.Base = dto.Base;
            unit.FactionAvailabilities = dto.FactionAvailabilities?.ToList();
            unit.UpdatedAt = DateTime.UtcNow;

            // Replace special abilities
            unit.UnitUnitSpecialAbilities.Clear();
            foreach (var abilityId in dto.UnitSpecialAbilityIds ?? Enumerable.Empty<Guid>()) {
                var ability = await _unitAbilityService.GetEntityByIdAsync(abilityId)
                              ?? throw new Exception($"UnitSpecialAbility {abilityId} not found");
                unit.UnitUnitSpecialAbilities.Add(new UnitUnitSpecialAbility { Unit = unit, UnitSpecialAbility = ability });
            }

            // Replace weapons
            unit.UnitWeapon!.Clear();
            foreach (var weaponId in dto.WeaponIds ?? Enumerable.Empty<Guid>()) {
                var weapon = await _weaponService.GetEntityByIdAsync(weaponId)
                             ?? throw new Exception($"Weapon {weaponId} not found");
                unit.UnitWeapon.Add(new UnitWeapon { Unit = unit, Weapon = weapon });
            }

            await _unitRepository.SaveAsync();
            return await GetFullByIdAsync(id);
        }
    }
}
