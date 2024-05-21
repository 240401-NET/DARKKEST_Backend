using DarkkestP3.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DarkkestP3.API.DB;

public class CommunityDBContext : DbContext
{
    public CommunityDBContext(){}
    public CommunityDBContext(DbContextOptions<CommunityDBContext> options) : base(options){}

    public virtual DbSet<Opportunity>  Opputunities{ get; set; }
    //public virtual DbSet<ModelName>  PluralName{get; set;}
    public virtual DbSet<Profile> Profiles{ get; set; }
}