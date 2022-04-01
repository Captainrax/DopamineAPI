using Dopamine_API_Data_DOTNET5;
using Dopamine_API_DOTNET5.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dopamine_API_DOTNET5
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // fixes recursive relation looping from EF Core >:L
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1", new OpenApiInfo { Title = "Dopamine_WebAPI", Version = "1" });
            });

            services.ConfigureSqlContext(Configuration);
            //services.AddEntityFrameworkSqlite();

            services.ConfigureRepositoryWrapper();

            services.AddCors(options =>  // LTPE Enable Cors
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .SetIsOriginAllowed((host) => true);
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
//#if Test_Logging
            , ILogger<Startup> logger, DopamineContext dataContext
            //#endif
            )
        {
            // Mapster
            //TypeAdapterConfig<Country, CountryDto>.NewConfig().Map(dest => dest.Cities1, src => src.Cities);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/1/swagger.json", "Dopamine_API_DOTNET5 1"));
            }

            //app.UseHttpsRedirection(); // disables auto redirect from http to https

            dataContext.Database.Migrate(); // auto migrates to sqlite on startup

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
