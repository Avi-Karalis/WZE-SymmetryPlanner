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
