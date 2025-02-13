using Template.Domain.Users;

namespace Template.Application.Repositories;


public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email);
}
