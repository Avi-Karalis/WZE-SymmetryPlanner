using Domain.Entities;

namespace Infrastructure.Interfaces {
    public interface IUserRepository {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByProviderIdAsync(string providerUserId);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task SaveAsync();
    }
}
