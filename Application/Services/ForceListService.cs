using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services {
    public class ForceListService: GenericService<ForceList, ForceListReadDto, ForceListCreateDto, ForceListUpdateDto>, IForceListService {
        private readonly IUnitService _unitService;
        private readonly IForceListRepository _forceListRepository;
        private readonly IMapper _mapper;

        public ForceListService(
            IForceListRepository forceListRepository,
            IUnitService unitService,
            IMapper mapper
        ) : base(forceListRepository, mapper) {
            _forceListRepository = forceListRepository;
            _unitService = unitService;
            _mapper = mapper;
        }
    }
}
