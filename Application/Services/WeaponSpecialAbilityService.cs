using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services {
    public class WeaponSpecialAbilityService : GenericService<WeaponSpecialAbility, WeaponSpecialAbilityReadDto, WeaponSpecialAbilityCreateDto, WeaponSpecialAbilityUpdateDto>, IWeaponSpecialAbilityService {
        private readonly IWeaponSpecialAbilityRepository _repository;
        private readonly IMapper _mapper;
        public WeaponSpecialAbilityService(IWeaponSpecialAbilityRepository repository, IMapper mapper) : base(repository, mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WeaponSpecialAbilityReadDto>> GetByNameAsync(string name) =>
            _mapper.Map<IEnumerable< WeaponSpecialAbilityReadDto>>(await _repository.GetByNameAsync(name));
    }
}
