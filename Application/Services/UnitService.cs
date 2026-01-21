using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services {
    public class UnitService : GenericService<Unit>, IUnitService {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitSpecialAbilityService _unitAbilityService;
        private readonly IWeaponService _weaponService;

        public UnitService(
            IUnitRepository unitRepository,
            IUnitSpecialAbilityService unitAbilityService,
            IWeaponService weaponService
        ) : base(unitRepository) {
            _unitRepository = unitRepository;
            _unitAbilityService = unitAbilityService;
            _weaponService = weaponService;
        }
        public async Task<Unit> GetFullByIdAsync(Guid id) {
            return await _unitRepository.GetFullByIdAsync(id);
        }

        public async Task<IEnumerable<Unit>> GetAllFullAsync() {
            return await _unitRepository.GetAllFullAsync();
        }

        public override async Task<Unit> CreateAsync(Unit unit) {
            // Process Unit Special Abilities
            var newAbilities = new List<UnitSpecialAbility>();
            foreach (var abilityEntry in unit.UnitUnitSpecialAbilities ?? Array.Empty<UnitUnitSpecialAbility>()) {
                UnitSpecialAbility ability;

                if (abilityEntry.UnitSpecialAbility.Id != Guid.Empty) {
                    ability = await _unitAbilityService.GetByIdAsync(abilityEntry.UnitSpecialAbility.Id)
                              ?? throw new Exception("UnitSpecialAbility not found");
                } else {
                    ability = await _unitAbilityService.CreateAsync(abilityEntry.UnitSpecialAbility);
                }

                newAbilities.Add(ability);
            }

            unit.AddUnitSpecialAbility(newAbilities);

            // Process Weapons
            foreach (var uw in unit.UnitWeapon ?? Array.Empty<UnitWeapon>()) {
                Weapon weapon;
                if (uw.Weapon.Id != Guid.Empty) {
                    weapon = await _weaponService.GetByIdAsync(uw.Weapon.Id)
                             ?? throw new Exception("Weapon not found");
                } else {
                    weapon = await _weaponService.CreateAsync(uw.Weapon);
                }

                unit.AddWeapon(weapon);
            }

            return await _unitRepository.CreateAsync(unit);
        }
    }
}
