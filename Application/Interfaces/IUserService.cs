using Domain.Entities;

namespace Application.Interfaces {
    public interface IUserService {
        Task<User> GetOrCreateAsync(string providerUserId, string email, string name, string? pictureUrl);
        Task<User?> GetByIdAsync(Guid id);
    }
}
