using DarkkestP3.API.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DarkkestP3.API.DB;

public class UserDBContext : IdentityDbContext<ApplicationUser>
{
    public UserDBContext() : base() {}
    public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) {}

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
}