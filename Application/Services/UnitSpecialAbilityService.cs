using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services {
    public class UnitSpecialAbilityService : GenericService<UnitSpecialAbility, UnitSpecialAbilityReadDto, UnitSpecialAbilityCreateDto, UnitSpecialAbilityUpdateDto>, IUnitSpecialAbilityService {
        private readonly IUnitSpecialAbilityRepository _repository;
        private readonly IMapper _mapper;
        public UnitSpecialAbilityService(IUnitSpecialAbilityRepository repository, IMapper mapper) : base(repository, mapper) {
            _repository = repository;
            _mapper = mapper;
        }

    }
}
