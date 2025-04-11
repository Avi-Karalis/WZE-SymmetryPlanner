using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity {
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

        public Task DeleteAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAndDeletedAsync() {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync() {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAndDeleted(Guid id) {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task HardDelete(Guid id) {
            throw new NotImplementedException();
        }

        public Task<T> RestoreAsync(Guid id) {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity) {
            throw new NotImplementedException();
        }
    }
}
