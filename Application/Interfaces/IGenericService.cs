using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces {
    public interface IGenericService<T> where T : class  {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T datum);
        Task<T> UpdateAsync(Guid id, T datum);
        Task<bool> DeleteAsync(Guid id);
    }
}
