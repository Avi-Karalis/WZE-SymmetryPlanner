using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.Services {
    public class WeaponService : GenericService<Weapon>, IWeaponService {
        private readonly IWeaponRepository _weaponRepository;
        private readonly IWeaponSpecialAbilityService _abilityService;

        public WeaponService(
            IWeaponRepository weaponRepository,
            IWeaponSpecialAbilityService abilityService
        ) : base(weaponRepository) {
            _weaponRepository = weaponRepository;
            _abilityService = abilityService;
        }

        public override async Task<Weapon> CreateAsync(Weapon weapon) {
            // Attach any special abilities (existing or new)
            var newAbilities = new List<WeaponWeaponSpecialAbility>();

            foreach (var ws in weapon.WeaponWeaponSpecialAbility ?? Array.Empty<WeaponWeaponSpecialAbility>()) {
                WeaponSpecialAbility ability;
                
                if (ws.WeaponSpecialAbility.Id != Guid.Empty) {
                    // Existing ability
                    ability = await _abilityService.GetByIdAsync(ws.WeaponSpecialAbility.Id)
                              ?? throw new Exception("WeaponSpecialAbility not found");
                } else {
                    // New ability
                    ability = await _abilityService.CreateAsync(ws.WeaponSpecialAbility);
                }

                newAbilities.Add(new WeaponWeaponSpecialAbility {
                    Weapon = weapon,
                    WeaponSpecialAbility = ability
                });
            }

            weapon.WeaponWeaponSpecialAbility = newAbilities;
            return await _weaponRepository.CreateAsync(weapon);
        }

        public async Task<Weapon> GetFullByIdAsync(Guid id) {
            return await _weaponRepository.GetFullByIdAsync(id);
        }


        public async Task<IEnumerable<Weapon>> GetAllFullAsync() {
            return await _weaponRepository.GetAllFullAsync();
        }
    }
}
