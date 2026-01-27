using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces {
    public interface IForceListRepository : IGenericRepository<ForceList> {
        Task<ForceList?> GetByIdAsync(Guid id);
        Task AddAsync(ForceList forceList);
        Task UpdateAsync(ForceList forceList);
    }
}
