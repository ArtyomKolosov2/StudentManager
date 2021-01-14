using Microsoft.AspNetCore.Identity;
using StudentManager_Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserConfirmationService : IUserConfirmation<User>
    {
        public Task<bool> IsConfirmedAsync(UserManager<User> manager, User user)
        {
            return Task.Run(() => user.IsRegistrationConfirmed);
        }
    }
}
