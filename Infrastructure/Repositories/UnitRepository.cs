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

        public async Task<Unit> GetFullByIdAsync(Guid id, bool includeTesting = false) {
            return await _context.Units
                .Include(u => u.UnitUnitSpecialAbilities)
                    .ThenInclude(uusa => uusa.UnitSpecialAbility)
                .Include(u => u.UnitWeapon)
                    .ThenInclude(uw => uw.Weapon)
                        .ThenInclude(w => w.WeaponWeaponSpecialAbility)
                            .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null && (u.Status == 0 || (includeTesting && u.Status == 1)))
                ?? throw new KeyNotFoundException($"Unit {id} not found");
        }
        public async Task<IEnumerable<Unit>> GetAllByFactionAsync(string faction, bool includeTesting = false) {
            return await _context.Units
                .Where(u => u.DeletedAt == null && (u.Status == 0 || (includeTesting && u.Status == 1)) && u.Faction == faction)
                .Include(u => u.UnitUnitSpecialAbilities)
                    .ThenInclude(uusa => uusa.UnitSpecialAbility)
                .Include(u => u.UnitWeapon)
                    .ThenInclude(uw => uw.Weapon)
                        .ThenInclude(w => w.WeaponWeaponSpecialAbility)
                            .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .ToListAsync();
        }

        public async Task<IEnumerable<Unit>> GetAlliesAsync(int allegianceType, bool includeTesting = false) {
            // allegianceType 0 = Light: Seconding or Advisor designations
            // allegianceType 1 = Darkness: Dark Cult designation
            var units = await _context.Units
                .Where(u => u.DeletedAt == null && (u.Status == 0 || (includeTesting && u.Status == 1)))
                .Include(u => u.UnitUnitSpecialAbilities)
                    .ThenInclude(uusa => uusa.UnitSpecialAbility)
                .Include(u => u.UnitWeapon)
                    .ThenInclude(uw => uw.Weapon)
                        .ThenInclude(w => w.WeaponWeaponSpecialAbility)
                            .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .ToListAsync();

            return allegianceType == 1
                ? units.Where(u => u.Designation != null && u.Designation.Any(d => d.ToLower() == "dark cult"))
                : units.Where(u => u.Designation != null && u.Designation.Any(d =>
                      d.ToLower() == "seconding" || d.ToLower() == "advisor"));
        }
        public async Task<IEnumerable<Unit>> GetAllFullAsync(bool includeTesting = false) {
            return await _context.Units
                .Where(u => u.DeletedAt == null && (u.Status == 0 || (includeTesting && u.Status == 1)))
                .Include(u => u.UnitUnitSpecialAbilities)
                    .ThenInclude(uusa => uusa.UnitSpecialAbility)
                .Include(u => u.UnitWeapon)
                    .ThenInclude(uw => uw.Weapon)
                        .ThenInclude(w => w.WeaponWeaponSpecialAbility)
                            .ThenInclude(wwsa => wwsa.WeaponSpecialAbility)
                .ToListAsync();
        }
        public async Task<List<string>> GetAvailableFactionsAsync(bool includeTesting = false) {
            return await _context.Units.Where(u => u.DeletedAt == null && (u.Status == 0 || (includeTesting && u.Status == 1)))
                .Select(u => u.Faction)
                .Distinct()
                .OrderBy(f => f)
                .ToListAsync();
        }

        public async Task<List<Unit>> GetUnitsByFactionAsync(string faction, bool includeTesting = false) {
            return await _context.Units.Where(u => u.DeletedAt == null && (u.Status == 0 || (includeTesting && u.Status == 1)) && u.Faction == faction)
                .ToListAsync();
        }
        public async Task<Unit> GetUnitTrackedAsync(Guid unitId) {
            return await _context.Units
                .FirstAsync(u => u.Id == unitId && u.DeletedAt == null);
        }
    }
}
