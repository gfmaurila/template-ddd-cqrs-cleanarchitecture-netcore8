using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Template.Infrastructure.Persistence.Entities;

namespace Template.Infrastructure.Persistence;
public class BasketAppDbContext : DbContext
{
    public BasketAppDbContext(DbContextOptions<BasketAppDbContext> options)
        : base(options) { }

    public DbSet<UserEntity> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
