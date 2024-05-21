using Darkkest.API.Model;
using Microsoft.EntityFrameworkCore;

public class CommunityDBContext : DbContext
{
    public CommunityDBContext(){}
    public CommunityDBContext(DbContextOptions<CommunityDBContext> options) : base(options){}

    public virtual DbSet<Opportunity>  Opputunities{get; set;}
    public virtual DbSet<Application>  Applications{get; set;}
    //public virtual DbSet<ModelName>  PluralName{get; set;}
}