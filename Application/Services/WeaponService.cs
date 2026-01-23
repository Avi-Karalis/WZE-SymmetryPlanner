using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services {
    public class WeaponService : GenericService<Weapon, WeaponReadDto, WeaponCreateDto, WeaponUpdateDto>, IWeaponService {
        private readonly IWeaponRepository _weaponRepository;
        private readonly IWeaponSpecialAbilityService _abilityService;
        private readonly IMapper _mapper;
        public WeaponService(
            IWeaponRepository weaponRepository,
            IWeaponSpecialAbilityService abilityService,
            IMapper mapper
        ) : base(weaponRepository, mapper) {
            _weaponRepository = weaponRepository;
            _abilityService = abilityService;
            _mapper = mapper;
        }

        public override async Task<WeaponReadDto> CreateAsync(WeaponCreateDto weaponCreateDto) {

            var newAbilities = new List<WeaponWeaponSpecialAbility>();
            List<Guid> weaponSpecialAbilities = new List<Guid>();
            foreach (var ws in weaponCreateDto.WeaponSpecialAbilityIds) {
                WeaponSpecialAbility ability;
                
                if (ws != Guid.Empty) {

                    ability = _mapper.Map<WeaponSpecialAbility>(await _abilityService.GetByIdAsync(ws))
                              ?? throw new Exception("WeaponSpecialAbility not found");
                    weaponSpecialAbilities.Add(ability.Id);
                } else {
                    ability = null!;
                }

                newAbilities.Add(new WeaponWeaponSpecialAbility {
                    Weapon = _mapper.Map<Weapon>(weaponCreateDto),
                    WeaponSpecialAbility = ability
                });
            }

            weaponCreateDto.WeaponSpecialAbilityIds = weaponSpecialAbilities;
            return _mapper.Map<WeaponReadDto>(await _weaponRepository.CreateAsync(_mapper.Map<Weapon>(weaponCreateDto)));
        }

        public async Task<WeaponReadDto> GetFullByIdAsync(Guid id) =>  _mapper.Map<WeaponReadDto>(await _weaponRepository.GetFullByIdAsync(id));
        


        public async Task<IEnumerable<WeaponReadDto>> GetAllFullAsync() => _mapper.Map<IEnumerable<WeaponReadDto>>(await _weaponRepository.GetAllFullAsync());
        
    }
}
