using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
namespace Application.Services {
    public class UnitService : GenericService<Unit>,IUnitService {
        private readonly IUnitRepository  _unitRepository;
        public UnitService(IUnitRepository repository) : base(repository) {
            _unitRepository = repository;
        }

        public async Task<IEnumerable<Unit>> GetByFactionAsync(string faction) {
            return await _unitRepository.GetByFactionAsync(faction);
        }


        public async Task<IEnumerable<Unit>> GetByDesignationAsync(string designation) {
            return await _unitRepository.GetByDesignationAsync(designation);
        }
    }
}
