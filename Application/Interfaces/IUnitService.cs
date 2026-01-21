using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces {
    public interface IUnitService : IGenericService<Unit> {
        Task<Unit> GetFullByIdAsync(Guid id);
        Task<IEnumerable<Unit>> GetAllFullAsync();
    }
}
