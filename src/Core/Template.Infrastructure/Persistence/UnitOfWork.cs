using Template.Application.Repositories;

namespace Template.Infrastructure.Persistence;

public class UnitOfWork(TemplateAppDbContext dbContext) : IUnitOfWork
{
    private readonly TemplateAppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Save changes to the database
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose() => _dbContext.Dispose();
}
