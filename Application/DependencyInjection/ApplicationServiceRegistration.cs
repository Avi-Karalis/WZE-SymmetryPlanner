using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DependencyInjection {
    public static class ApplicationServiceRegistration {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection) {
            serviceCollection.AddScoped(typeof(IGenericService<>), typeof(IGenericService<>));
        
            return serviceCollection;
        }
    }
}
