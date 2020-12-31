using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManager_Core.Identity;

namespace Infrastructure.Context
{
    public class StudentManagerDbContext : IdentityDbContext<User>
    {
        public StudentManagerDbContext(DbContextOptions<StudentManagerDbContext> options) : base(options) { }
    }
}
