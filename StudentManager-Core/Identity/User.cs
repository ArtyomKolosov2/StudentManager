using Microsoft.AspNetCore.Identity;
using StudentManager_Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager_Core.Identity
{
    public class User : IdentityUser, IHasId
    {
        public string DisplayFirstName { get; set; }
        public string DisplayLastName { get; set; }
        public bool IsRegistrationConfirmed { get; set; }
    }
}
