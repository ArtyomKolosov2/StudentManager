using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManager_Core.Entities;
using StudentManager_Core.Identity;

namespace Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<UserConfirmation> Confirmations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
