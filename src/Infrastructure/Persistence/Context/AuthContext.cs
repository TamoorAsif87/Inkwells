using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.Context;

public class AuthContext:IdentityDbContext<ApplicationUser>
{
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public AuthContext(DbContextOptions<AuthContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        

        base.OnModelCreating(builder);
    }
}
