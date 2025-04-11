using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
namespace Infrastructure.Interfaces {
    public interface IGenericRepository<T> where T : IEntity{
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAndDeleted(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAndDeletedAsync();
        Task<T> RestoreAsync(Guid id);
        Task HardDelete(Guid id);
    }
}
