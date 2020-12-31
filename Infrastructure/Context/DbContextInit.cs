using Microsoft.AspNetCore.Identity;
using StudentManager_Core.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class DbContextInit
    {
        public static async Task SeedDataAsync(StudentManagerDbContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<User>
                      {
                        new User
                            {
                              UserName = "TestUserFirst",
                              Email = "testuserfirst@test.com"
                            },
                        new User
                            {
                              UserName = "TestUserSecond",
                              Email = "testusersecond@test.com"
                             }
                         };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "qazwsX123@");
                }
            }
        }
    }
}
