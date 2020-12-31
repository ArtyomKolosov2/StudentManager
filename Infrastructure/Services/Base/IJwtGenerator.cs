using StudentManager_Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Base
{
    public interface IJwtGenerator
    {
        string CreateJwtToken(User user);
    }
}
