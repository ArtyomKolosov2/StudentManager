using Infrastructure.Context;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManager_Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions.ServiceCollection.Identity
{
    public static class IdentityMiddlewareConfigure
    {
        public static void AddIdentityContext(this IServiceCollection services, string connectionString) 
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password = new PasswordOptions
                {
                    RequiredLength = 6,
                    RequireNonAlphanumeric = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequireDigit = false,
                };
                opts.User.RequireUniqueEmail = true;
                opts.SignIn.RequireConfirmedAccount = true;
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddUserConfirmation<UserConfirmationService>();
        }
    }
}
