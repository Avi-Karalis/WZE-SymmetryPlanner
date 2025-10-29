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
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<Unit>> GetByFactionAsync(string faction) {
            return await _context.Units
                                 .Where(u => u.Faction == faction && u.DeletedAt == null)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Unit>> GetByDesignationAsync(string designation) {
            return await _context.Units
                                 .Where(u => u.Designation.Contains(designation) && u.DeletedAt == null)
                                 .ToListAsync();
        }
    }
}
