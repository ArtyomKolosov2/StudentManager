using Infrastructure.Context;
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
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<UserDbContext>();
        }
    }
}
