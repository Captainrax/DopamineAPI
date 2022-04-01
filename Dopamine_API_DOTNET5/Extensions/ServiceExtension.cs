using Dopamine_API_Data_DOTNET5;
using Dopamine_API_Data_DOTNET5.Interfaces;
using Dopamine_API_Data_DOTNET5.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dopamine_API_DOTNET5.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["connectionStrings:DBConnectionString"];
            services.AddDbContext<DopamineContext>(o => o.UseSqlite(connectionString, x => x.MigrationsAssembly("Dopamine_API_DOTNET5")));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
