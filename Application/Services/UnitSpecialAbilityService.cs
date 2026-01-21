using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services {
    public class UnitSpecialAbilityService : GenericService<UnitSpecialAbility>, IUnitSpecialAbilityService {
        private readonly IUnitSpecialAbilityRepository _repository;

        public UnitSpecialAbilityService(IUnitSpecialAbilityRepository repository) : base(repository) {
            _repository = repository;
        }

        public async Task<IEnumerable<UnitSpecialAbility>> GetByNameAsync(string name) =>
            await _repository.GetByNameAsync(name);
    }
}
