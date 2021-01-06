using Infrastructure.Context;
using Infrastructure.Extensions.ServiceCollection.Auth;
using Infrastructure.Extensions.ServiceCollection.Dependencies;
using Infrastructure.Extensions.ServiceCollection.Identity;
using Infrastructure.Extensions.ServiceCollection.Swagger;
using Infrastructure.Services;
using Infrastructure.Services.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentManager_Core.Identity;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace StudentManager_API
{
    /// <summary>
    /// Class with logic for configuring services and middleware
    /// </summary>
    public class Startup
    {
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// IConfiguration property
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var userConnection = Configuration.GetConnectionString("UserConnection");

            services.AddControllers();
            services.AddCors();
            services.AddIdentityContext(userConnection);
            services.AddDependenciesInjections();
            services.AddAuth(Configuration["JwtTokenKey"]);
            services.AddSwaggerDocs(Assembly.GetExecutingAssembly());
            
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentManager_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
