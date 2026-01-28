using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces {
    public interface IForceListRepository : IGenericRepository<ForceList> {
        Task AddAsync(ForceList forceList);
        Task<ForceList> GetByIdWithUnitsAsync(Guid id);
        Task<Unit> GetUnitByIdAsync(Guid unitId);

    }
}
