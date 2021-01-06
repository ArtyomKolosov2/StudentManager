using Infrastructure.Services;
using Infrastructure.Services.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions.ServiceCollection.Dependencies
{
    public static class DependenciesMiddlewareConfigure
    {
        public static void AddDependenciesInjections(this IServiceCollection services)
        {
            services.AddScoped<IJwtGenerator, JwtGenerator>();
        }
    }
}
