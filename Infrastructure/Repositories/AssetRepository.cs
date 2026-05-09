using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infrastructure.Repositories {
    public class AssetRepository : GenericRepository<Asset>, IAssetRepository {
        private readonly ApplicationDbContext _context;

        public AssetRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllByFactionAsync(string faction) {
            IEnumerable<Asset> asset;
            if (faction.Contains("Dark Legion -")) { 
                asset = await _context.Assets
                    .Where(a => a.DeletedAt == null && a.Status == 0 && (a.Faction == faction || a.Faction == "Necrotech"))
                    .ToListAsync();
            } else {
                asset = await _context.Assets
                    .Where(a => a.DeletedAt == null && a.Status == 0 && (a.Faction == faction || a.Faction == "General"))
                    .ToListAsync();
            }
            return asset;
        }
    }
}
