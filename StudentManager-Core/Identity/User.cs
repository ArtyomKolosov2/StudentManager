using Microsoft.AspNetCore.Identity;

namespace StudentManager_Core.Identity
{
    public class User : IdentityUser
    {
        public string Token { get; set; }
        public string DisplayFirstName { get; set; }
        public string DisplayLastName { get; set; }
    }
}
