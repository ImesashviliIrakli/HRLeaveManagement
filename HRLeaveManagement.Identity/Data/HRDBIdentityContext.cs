using HRLeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Identity.Data;

public class HRDBIdentityContext : IdentityDbContext<ApplicationUser>
{
    public HRDBIdentityContext(DbContextOptions<HRDBIdentityContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HRDBIdentityContext).Assembly);

    }
}
