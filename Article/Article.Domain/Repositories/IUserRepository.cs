using Article.Domain.Commands.User;
using Article.Domain.Entities;
using Article.Domain.Queries;
using System;
using System.Threading.Tasks;

namespace Article.Domain.Repositories
{
    public interface IUserRepository
    {
        ValueTask<UserQuery> GetUser(Guid user_ID);
        ValueTask<bool> RegisterUser(User user);
        ValueTask<bool> UpdateUser(UpdateUserCommand user);
        ValueTask<bool> DeleteUser(DeleteUserCommand user);
    }
}
