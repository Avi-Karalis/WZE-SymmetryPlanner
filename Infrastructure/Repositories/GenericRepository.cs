using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;


namespace Infrastructure.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context) {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T> CreateAsync(T entity) {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id) {
            T entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id) ??
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            entity.DeletedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            var result = _dbSet.Update(entity);
            if (result != null) {
                await _context.SaveChangesAsync();
                return true;
            }
            await _context.SaveChangesAsync();
            return false;
        }

        public async Task<IEnumerable<T>> GetAllAndDeletedAsync() =>
            await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.Where(e=> e.DeletedAt == null).ToListAsync();

        public async Task<T> GetByIdAndDeleted(Guid id) {
            T entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            return entity ?? throw new KeyNotFoundException($"Entity with ID {id} not found or is deleted.");
        }

        public async Task<T> GetByIdAsync(Guid id) {
            T entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.DeletedAt == null);
            return entity ?? throw new KeyNotFoundException($"Entity with ID {id} not found or is deleted.");
        }

        public async Task<bool> HardDelete(Guid id) {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id) ??
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            var result = _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<T> RestoreAsync(Guid id) {
            T entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id) ??
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            entity.DeletedAt = null;
            entity.UpdatedAt = DateTime.UtcNow;
            var result = _dbSet.Update(entity);
            if (result != null) {
                await _context.SaveChangesAsync();
                return entity;
            }
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity) {
            var existing = await _dbSet
                    .FirstOrDefaultAsync(e => e.Id == entity.Id)
                    ?? throw new KeyNotFoundException($"Entity with ID {entity.Id} not found.");

            _context.Entry(existing).CurrentValues.SetValues(entity);

            // Handle ICollection<T> properties
            var collectionProps = typeof(T).GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                            typeof(ICollection<>).IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition()));

            foreach (var prop in collectionProps) {
                var existingCollection = (IList)prop.GetValue(existing);
                var newCollection = (IEnumerable)prop.GetValue(entity);

                existingCollection.Clear();
                foreach (var item in newCollection) {
                    existingCollection.Add(item);
                }
            }

            existing.GetType().GetProperty("UpdatedAt")?.SetValue(existing, DateTime.UtcNow);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
