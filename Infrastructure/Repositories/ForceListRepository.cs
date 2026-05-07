using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories {
    public class ForceListRepository :GenericRepository<ForceList>, IForceListRepository {
        private readonly ApplicationDbContext _context;

        public ForceListRepository(ApplicationDbContext context) : base(context){
            _context = context;
        }


        public async Task AddAsync(ForceList forceList) {
            _context.ForceLists.Add(forceList);
            await _context.SaveChangesAsync();
        }
        public async Task<ForceList> GetByIdWithUnitsAsync(Guid id) {
            return await _context.ForceLists
                .Include(f => f.ForceListUnits)
                    .ThenInclude(flu => flu.Unit)
                        .ThenInclude(u => u.UnitWeapon)
                .Include(f => f.ForceListUnits)
                    .ThenInclude(flu => flu.Unit)
                        .ThenInclude(u => u.UnitUnitSpecialAbilities)
                .FirstOrDefaultAsync(f => f.Id == id && f.DeletedAt == null)
                ?? throw new KeyNotFoundException("Force list not found");
        }
        public async Task<Unit> GetUnitByIdAsync(Guid unitId) {
            return await _context.Units
                .Include(u => u.UnitWeapon)
                .Include(u => u.UnitUnitSpecialAbilities)
                .FirstAsync(u => u.Id == unitId && u.DeletedAt == null);
        }

    }

}
