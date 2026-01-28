using Domain.Interfaces;
namespace Infrastructure.Interfaces {
    public interface IGenericRepository<T> where T : IEntity{
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAndDeleted(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAndDeletedAsync();
        Task<T> RestoreAsync(Guid id);
        Task<bool> HardDelete(Guid id);
        Task SaveAsync();
    }
}
