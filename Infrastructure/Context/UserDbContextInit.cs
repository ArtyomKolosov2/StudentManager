using Microsoft.AspNetCore.Identity;
using StudentManager_Core.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Data.Constants.IdentityRoleConstants;

namespace Infrastructure.Context
{
    public class UserDbContextInit
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                
                if (await roleManager.FindByNameAsync(ADMIN) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole(ADMIN));
                }
                if (await roleManager.FindByNameAsync(MODERATOR) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole(MODERATOR));
                }
                if (await roleManager.FindByNameAsync(DEV) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole(DEV));
                }
                if (await roleManager.FindByNameAsync(TEACHER) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole(TEACHER));
                }
                if (await roleManager.FindByNameAsync(STUDENT) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole(STUDENT));
                }
            }
        }
        public static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var adminEmail = "admin@gmail.com";
                var password = "123456";
                await CreateUser(adminEmail, password, ADMIN, userManager);
                await CreateUser("student1@mail.ru", "123456", STUDENT, userManager);
                await CreateUser("student2@mail.ru", "123456", STUDENT, userManager);
                await CreateUser("dev1@mail.ru", "123456", DEV, userManager);
                await CreateUser("dev2@mail.ru", "123456", DEV, userManager);
            }
            

        }
        private static async Task CreateUser(string email, string password, string role, UserManager<User> userManager)
        {
            if (await userManager.FindByNameAsync(email) is null)
            {
                var user = new User { Email = email, UserName = email };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
