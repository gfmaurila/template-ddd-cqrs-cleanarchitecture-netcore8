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

    public async Task UpdateAsync(User entity)
    {
        var existingEntity = await _context.User.FindAsync(entity.Id.Value);
        if (existingEntity == null)
            throw new InvalidOperationException($"User with ID {entity.Id.Value} not found.");

        // Atualizar manualmente as propriedades
        _mapper.Map(entity, existingEntity);

        // Anexar a entidade existente ao contexto
        _context.Attach(existingEntity);
        _context.Entry(existingEntity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.User.FindAsync(id);
        if (entity != null)
        {
            _context.User.Remove(entity);
        }
    }
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
