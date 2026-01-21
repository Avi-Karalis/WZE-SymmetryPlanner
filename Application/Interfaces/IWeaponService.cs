using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces {
    public interface IWeaponService : IGenericService<Weapon>{
        Task<Weapon> GetFullByIdAsync(Guid id);
        Task<IEnumerable<Weapon>> GetAllFullAsync();
    }
}
