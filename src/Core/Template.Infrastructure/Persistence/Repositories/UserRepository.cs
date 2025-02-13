using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Template.Application.Repositories;
using Template.Domain.Users;
using Template.Infrastructure.Persistence.Entities;

namespace Template.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TemplateAppDbContext _context;
    private readonly IMapper _mapper;
    public UserRepository(TemplateAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #region Commands
    public async Task AddAsync(User entity, CancellationToken cancellationToken)
        => await _context.AddAsync(_mapper.Map<UserEntity>(entity));

    public Task UpdateAsync(User entity)
        => Task.FromResult(_context.Update(entity));

    public Task DeleteAsync(Guid id)
        => Task.FromResult(_context.Remove(id));
    #endregion

    #region Queries
    public async Task<bool> ExistsByEmailAsync(string email)
        => await _context.User.AsNoTracking().AnyAsync(entity => entity.Email == email);

    public async Task<List<User>> GetAllAsync()
        => _mapper.Map<List<User>>(await _context.User.AsNoTracking().ToListAsync());

    public async Task<User?> GetByIdAsync(Guid id)
        => _mapper.Map<User>(await _context.User
                         .Where(b => b.Id == id)
                         .FirstOrDefaultAsync());
    public async Task<bool> IsExistsAsync(Guid id)
        => (await _context.User.FirstOrDefaultAsync(x => x.Id == id)) != null;
    #endregion






}
