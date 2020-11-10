using Article.Domain.Commands.User;
using Article.Domain.Entities;
using Article.Domain.Queries;
using Article.Domain.Repositories;
using Article.Infra.DataContext;
using Dapper;
using System;
using System.Threading.Tasks;

namespace Article.Infra.Repositories
{
    public class UserRepository : IUserRepository

    {
        private readonly ArticleDataContext _context;
        public UserRepository(ArticleDataContext context)
        {
            _context = context;
        }

        public async ValueTask<UserQuery> GetUser(Guid user_ID)
        {

            string query = @" SELECT * FROM Users                              
                               WHERE User_ID = @user_ID ";

            return await _context.Connection.QuerySingleOrDefaultAsync<UserQuery>(query,
                  new { User_ID = user_ID });

        }

        public async ValueTask<bool> RegisterUser(User user)
        {
            int rows = 0;

            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    string query = @" INSERT INTO Users VALUES (@user_ID,@Name,@middle_name) ";

                    rows = await _context.Connection.ExecuteAsync(query,
                          user, transaction);

                    transaction.Commit();
                }

                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return rows > 0;
        }

        public async ValueTask<bool> UpdateUser(UpdateUserCommand user)
        {
            int rows = 0;

            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    string query = @" UPDATE Users SET Name = @Name, Middle_Name = @middle_name WHERE User_ID = @user_ID   ";

                    rows = await _context.Connection.ExecuteAsync(query,
                          user, transaction);

                    transaction.Commit();
                }

                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return rows > 0;
        }


        public async ValueTask<bool> DeleteUser(DeleteUserCommand user)
        {
            int rows = 0;

            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    string query = @" DELETE FROM Users  WHERE User_ID = @user_ID ";

                    rows = await _context.Connection.ExecuteAsync(query,
                          user, transaction);

                    transaction.Commit();
                }

                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return rows > 0;
        }
    }
}
