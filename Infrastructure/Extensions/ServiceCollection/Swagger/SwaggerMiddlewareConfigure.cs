using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Infrastructure.Extensions.ServiceCollection.Swagger
{
    public static class SwaggerMiddlewareConfigure
    {
        public static void AddSwaggerDocs(this IServiceCollection services, Assembly assembly)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "StudentManager_API",
                    Version = "v1",
                    Description = "A simple student managment system",
                    Contact = new OpenApiContact
                    {
                        Email = "artyomkolosov2@gmail.com",
                        Name = "Artyom Kolosov",
                        Url = new Uri("https://www.linkedin.com/in/artyom-kolosov/")
                    }
                });
                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                              Reference = new OpenApiReference
                              {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                              }
                        },
                        Array.Empty<string>()
                    }
                });

            });
        }
    }
}
