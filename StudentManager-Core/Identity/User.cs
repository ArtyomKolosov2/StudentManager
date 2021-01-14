using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager_Core.Identity
{
    public class User : IdentityUser
    {
        public string DisplayFirstName { get; set; }
        public string DisplayLastName { get; set; }
        public bool IsRegistrationConfirmed { get; set; }
        public string Image { get; set; }
    }
}
