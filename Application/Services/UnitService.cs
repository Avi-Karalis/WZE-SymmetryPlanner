using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;


namespace Application.Services {
    public class UnitService : GenericService<Unit, UnitReadDto, UnitCreateDto, UnitUpdateDto>, Interfaces.IUnitService {
        private readonly Infrastructure.Interfaces.IUnitRepository _unitRepository;
        private readonly IUnitSpecialAbilityService _unitAbilityService;
        private readonly IWeaponService _weaponService;
        private readonly IMapper _mapper;
        public UnitService(
            Infrastructure.Interfaces.IUnitRepository unitRepository,
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
    }
}
