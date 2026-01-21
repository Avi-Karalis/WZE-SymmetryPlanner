using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services {
    public class WeaponSpecialAbilityService : GenericService<WeaponSpecialAbility>, IWeaponSpecialAbilityService {
        private readonly IWeaponSpecialAbilityRepository _repository;

        public WeaponSpecialAbilityService(IWeaponSpecialAbilityRepository repository) : base(repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<WeaponSpecialAbility>> GetByNameAsync(string name) =>
            await _repository.GetByNameAsync(name);
    }
}
