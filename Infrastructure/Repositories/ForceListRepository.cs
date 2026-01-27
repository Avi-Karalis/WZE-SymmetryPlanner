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

        public async Task<ForceList?> GetByIdAsync(Guid id) {
            return await _context.ForceLists
                .Include(f => f.Units)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(ForceList forceList) {
            _context.ForceLists.Add(forceList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForceList forceList) {
            _context.ForceLists.Update(forceList);
            await _context.SaveChangesAsync();
        }
    }

}
