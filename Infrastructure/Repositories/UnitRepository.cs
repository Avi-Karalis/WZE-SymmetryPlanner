using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories {
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public async Task<Unit> GetFullByIdAsync(Guid id) {
            return await _context.Units
                .Include(u => u.UnitUnitSpecialAbilities)
                    .ThenInclude(uusa => uusa.UnitSpecialAbility)
                .Include(u => u.UnitWeapon)
                    .ThenInclude(uw => uw.Weapon)
                        .ThenInclude(w => w.WeaponWeaponSpecialAbility)
                            .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null && u.Status == 0)
                ?? throw new KeyNotFoundException($"Unit {id} not found");
        }
        public async Task<IEnumerable<Unit>> GetAllByFactionAsync(string faction) {
            return await _context.Units
                .Where(u => u.DeletedAt == null && u.Status == 0 && u.Faction == faction)
                .Include(u => u.UnitUnitSpecialAbilities)
                    .ThenInclude(uusa => uusa.UnitSpecialAbility)
                .Include(u => u.UnitWeapon)
                    .ThenInclude(uw => uw.Weapon)
                        .ThenInclude(w => w.WeaponWeaponSpecialAbility)
                            .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .ToListAsync();
        }
        public async Task<IEnumerable<Unit>> GetAllFullAsync() {
            return await _context.Units
                .Where(u => u.DeletedAt == null && u.Status == 0)
                .Include(u => u.UnitUnitSpecialAbilities)
                    .ThenInclude(uusa => uusa.UnitSpecialAbility)
                .Include(u => u.UnitWeapon)
                    .ThenInclude(uw => uw.Weapon)
                        .ThenInclude(w => w.WeaponWeaponSpecialAbility)
                            .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .ToListAsync();
        }
        public async Task<List<string>> GetAvailableFactionsAsync() {
            return await _context.Units.Where(u => u.DeletedAt == null && u.Status ==0)
                .Select(u => u.Faction)
                .Distinct()
                .OrderBy(f => f)
                .ToListAsync();
        }

        public async Task<List<Unit>> GetUnitsByFactionAsync(string faction) {
            return await _context.Units.Where(u => u.DeletedAt == null && u.Status == 0 && u.Faction == faction)
                .ToListAsync();
        }
        public async Task<Unit> GetUnitTrackedAsync(Guid unitId) {
            return await _context.Units
                .FirstAsync(u => u.Id == unitId && u.DeletedAt == null);
        }
    }
}
