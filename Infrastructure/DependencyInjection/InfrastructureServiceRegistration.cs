using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.DependencyInjection {
    public static class InfrastructureServiceRegistration {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository <>));
            return services;
        }
    }
}