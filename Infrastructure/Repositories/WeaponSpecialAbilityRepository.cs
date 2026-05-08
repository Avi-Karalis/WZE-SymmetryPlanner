using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories {
    public class WeaponSpecialAbilityRepository : GenericRepository<WeaponSpecialAbility>, IWeaponSpecialAbilityRepository {
        private readonly ApplicationDbContext _context;
        public WeaponSpecialAbilityRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<WeaponSpecialAbility>> GetByNameAsync(string name) =>
            await _context.WeaponSpecialAbilities
                .Where(w => w.Name.Contains(name) && w.DeletedAt == null)
                .ToListAsync();

        public async Task<WeaponSpecialAbility?> GetByIdAsync(Guid id) =>
            await _context.WeaponSpecialAbilities
                .FirstOrDefaultAsync(w => w.Id == id && w.DeletedAt == null);
    }


}
