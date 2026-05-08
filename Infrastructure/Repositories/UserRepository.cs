using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories {
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) {
            _context = context;
        }

        public Task<User?> GetByIdAsync(Guid id) =>
            _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        public Task<User?> GetByProviderIdAsync(string providerUserId) =>
            _context.Users.FirstOrDefaultAsync(u => u.ProviderUserId == providerUserId);

        public Task<User?> GetByEmailAsync(string email) =>
            _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public Task<IEnumerable<User>> GetAllAsync() =>
            Task.FromResult<IEnumerable<User>>(_context.Users.AsEnumerable());

        public async Task AddAsync(User user) =>
            await _context.Users.AddAsync(user);

        public Task SaveAsync() =>
            _context.SaveChangesAsync();
    }
}
