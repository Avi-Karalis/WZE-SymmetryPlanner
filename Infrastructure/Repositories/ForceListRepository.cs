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

    }

}
