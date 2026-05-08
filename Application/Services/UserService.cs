using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Application.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private const string SuperAdminEmail = "Averkkaralis@gmail.com";

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public Task<User?> GetByIdAsync(Guid id) => _userRepository.GetByIdAsync(id);

        public Task<IEnumerable<User>> GetAllAsync() => _userRepository.GetAllAsync();

        public async Task<User> UpdateRoleAsync(Guid userId, RoleType role) {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found.");
            // Prevent changing the SuperAdmin's role
            if (user.Email.Equals(SuperAdminEmail, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Cannot change the role of the super admin.");
            user.Role = role;
            await _userRepository.SaveAsync();
            return user;
        }

        public async Task<User> GetOrCreateAsync(string providerUserId, string email, string name, string? pictureUrl) {
            var user = await _userRepository.GetByProviderIdAsync(providerUserId);

            if (user is null) {
                user = new User(providerUserId, email, name) {
                    PictureUrl = pictureUrl,
                    Role = email.Equals(SuperAdminEmail, StringComparison.OrdinalIgnoreCase)
                        ? RoleType.SuperAdmin
                        : RoleType.User,
                    LastLogin = DateTime.UtcNow
                };
                await _userRepository.AddAsync(user);
            } else {
                user.LastLogin = DateTime.UtcNow;
                user.PictureUrl = pictureUrl;
                // Ensure super admin stays super admin even if edited
                if (email.Equals(SuperAdminEmail, StringComparison.OrdinalIgnoreCase))
                    user.Role = RoleType.SuperAdmin;
            }

            await _userRepository.SaveAsync();
            return user;
        }
    }
}
