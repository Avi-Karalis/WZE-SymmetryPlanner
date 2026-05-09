using Application.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;


namespace Application.Services {
    public class AssetService : GenericService<Asset, AssetReadDTO, AssetCreateDTO, AssetUpdateDTO>, IAssetService {
        private readonly IAssetRepository _assetRepository;
        private readonly IMapper _mapper;

        public AssetService(
            IAssetRepository assetRepository,
            IMapper mapper
        ) : base(assetRepository, mapper) {
            _assetRepository = assetRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AssetReadDTO>> GetAllByFactionAsync(string faction) {
            var assets = await _assetRepository.GetAllByFactionAsync(faction);

            return _mapper.Map<IEnumerable<AssetReadDTO>>(assets);
        }


    }
}
