using AutoMapper;
using Template.Application.Repositories;
using Template.Domain.Users;

namespace Template.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TemplateAppDbContext _dbContext;
    private readonly IMapper _mapper;
    public UserRepository(TemplateAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    #region Commands
    public Task AddAsync(User entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Queries
    public Task<bool> ExistsByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    #endregion





    
}
