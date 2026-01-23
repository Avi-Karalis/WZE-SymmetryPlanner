using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Application.DependencyInjection {
    public static class ApplicationServiceRegistration {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection) {
            serviceCollection.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
            serviceCollection.AddScoped(typeof(IUnitService), typeof(UnitService));
            serviceCollection.AddScoped(typeof(IWeaponService), typeof(WeaponService));
            serviceCollection.AddScoped(typeof(IWeaponSpecialAbilityService), typeof(WeaponSpecialAbilityService));
            serviceCollection.AddScoped(typeof(IUnitSpecialAbilityService), typeof(UnitSpecialAbilityService));

            return serviceCollection;
        }
    }
}
