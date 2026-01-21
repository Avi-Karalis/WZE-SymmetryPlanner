using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.DependencyInjection;
    public static class InfrastructureServiceRegistration {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b=>b.MigrationsAssembly("Infrastructure"))
            );
            
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository <>));
            services.AddScoped(typeof(IUnitRepository), typeof(UnitRepository));
            services.AddScoped(typeof(IWeaponRepository), typeof(WeaponRepository));
            services.AddScoped(typeof(IUnitSpecialAbilityRepository), typeof(UnitSpecialAbilityRepository));
            services.AddScoped(typeof(IWeaponSpecialAbilityRepository), typeof(WeaponSpecialAbilityRepository));
            return services;
        }
    }
