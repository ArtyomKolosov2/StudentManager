using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager_Core.Identity
{
    public class User : IdentityUser
    {
        public string Token { get; set; }
    }
}
