using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
namespace Application.Services {
    public class GenericService<TEntity, TReadDto, TCreateDto, TUpdateDto> : IGenericService<TEntity, TReadDto, TCreateDto, TUpdateDto> where TEntity : BaseEntity {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public virtual async Task<TReadDto> CreateAsync(TCreateDto dto) {

            var entity = _mapper.Map<TEntity>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<TReadDto>(created);
        }
        public async Task<IEnumerable<TReadDto>> GetAllAsync() {
            return _mapper.Map<IEnumerable<TReadDto>>(await _repository.GetAllAsync());
        }

        public async Task<IEnumerable<TReadDto>> GetAllAndDeletedAsync() {
            return _mapper.Map<IEnumerable<TReadDto>>(await _repository.GetAllAndDeletedAsync());
        }

        public async Task<TReadDto?> GetByIdAsync(Guid id) {

            return _mapper.Map<TReadDto>(await _repository.GetByIdAsync(id));
        }

        public async Task<TReadDto> GetByIdAndDeletedAsync(Guid id) {
            return _mapper.Map<TReadDto>(await _repository.GetByIdAndDeleted(id));
        }

        public async Task<TReadDto> UpdateAsync(Guid id, TUpdateDto dto) {
            var entity = _mapper.Map<TEntity>(dto);

            // Ensure the entity ID matches
            if (entity.Id != id)
                entity.Id = id;
            return _mapper.Map<TReadDto>(await _repository.UpdateAsync(entity));
        }
        public async Task<bool> DeleteAsync(Guid id) {
            return await _repository.DeleteAsync(id);
        }
        public async Task<bool> HardDeleteAsync(Guid id) {
            return await _repository.HardDelete(id);
        }
        public async Task<TReadDto> RestoreAsync(Guid id) {
            return _mapper.Map<TReadDto>(await _repository.RestoreAsync(id));
        }

    }
}
