using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
namespace Application.Services {
    public class GenericService<T> : IGenericService<T> where T : BaseEntity {
        private readonly IGenericRepository<T> _repository;
        public GenericService(IGenericRepository<T> repository) => _repository = repository;

        public async Task<T> CreateAsync(T entity) {
            entity.CreatedAt = DateTime.UtcNow;
            return await _repository.CreateAsync(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync() {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> GetAllAndDeletedAsync() {
            return await _repository.GetAllAndDeletedAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id) {
            try {
                return await _repository.GetByIdAsync(id);
            } catch (KeyNotFoundException) {
                return null;
            }
        }

        public async Task<T> GetByIdAndDeletedAsync(Guid id) {
            return await _repository.GetByIdAndDeleted(id);
        }

        public async Task<T> UpdateAsync(Guid id, T entity) {
            // Ensure the entity ID matches
            if (entity.Id != id)
                entity.Id = id;
            return await _repository.UpdateAsync(entity);
        }
        public async Task<bool> DeleteAsync(Guid id) {
            return await _repository.DeleteAsync(id);
        }
        public async Task<bool> HardDeleteAsync(Guid id) {
            return await _repository.HardDelete(id);
        }
        public async Task<T> RestoreAsync(Guid id) {
            return await _repository.RestoreAsync(id);
        }
    }
}
