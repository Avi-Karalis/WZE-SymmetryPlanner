using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories;
public class UnitSpecialAbilityRepository : GenericRepository<UnitSpecialAbility>, IUnitSpecialAbilityRepository {
    private readonly ApplicationDbContext _context;
    public UnitSpecialAbilityRepository(ApplicationDbContext context) : base(context) { _context = context; }

    public async Task<IEnumerable<UnitSpecialAbility>> GetByNameAsync(string name) =>
        await _context.UnitSpecialAbilities
            .Where(u => u.Name.Contains(name) && u.DeletedAt == null)
            .ToListAsync();

    public async Task<UnitSpecialAbility?> GetByIdAsync(Guid id) =>
        await _context.UnitSpecialAbilities
            .FirstOrDefaultAsync(u => u.Id == id && u.DeletedAt == null);
}