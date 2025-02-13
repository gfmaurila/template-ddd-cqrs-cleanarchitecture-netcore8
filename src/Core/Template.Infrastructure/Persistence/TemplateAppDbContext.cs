using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Template.Infrastructure.Persistence.Entities;

namespace Template.Infrastructure.Persistence;
public class TemplateAppDbContext : DbContext
{
    public TemplateAppDbContext(DbContextOptions<TemplateAppDbContext> options)
        : base(options) { }

    public DbSet<UserEntity> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
