using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories {
    public class WeaponRepository : GenericRepository<Weapon>, IWeaponRepository {
        private readonly ApplicationDbContext _context;

        public WeaponRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }
        public async Task<Weapon> GetFullByIdAsync(Guid id) {
            return await _context.Weapons
                .Include(w => w.WeaponWeaponSpecialAbility)
                    .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .FirstOrDefaultAsync(w => w.Id == id && w.DeletedAt == null)
                ?? throw new KeyNotFoundException($"Weapon {id} not found");
        }

        public async Task<IEnumerable<Weapon>> GetAllFullAsync() {
            return await _context.Weapons
                .Where(w => w.DeletedAt == null)
                .Include(w => w.WeaponWeaponSpecialAbility)
                    .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .ToListAsync();
        }
    }
}
