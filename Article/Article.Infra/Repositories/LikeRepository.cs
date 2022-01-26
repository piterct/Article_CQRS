using Article.Domain.Commands.Like;
using Article.Domain.Commands.User;
using Article.Domain.Entities;
using Article.Domain.Queries;
using Article.Domain.Queries.Like;
using Article.Domain.Repositories;
using Article.Infra.DataContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Article.Infra.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ArticleDataContext _context;
        public LikeRepository(ArticleDataContext context)
        {
            _context = context;
        }

        public async ValueTask<GetQuantityLikeQuery> GetQuantityLikesByArticle(Guid article_ID)
        {
            string query = @" SELECT DISTINCT COUNT(Liked) QuantityLikes FROM Likes                              
                               WHERE liked = 1 and Article_ID = @article_ID ";

            return await _context.Connection.QuerySingleOrDefaultAsync<GetQuantityLikeQuery>(query,
                  new { Article_ID = article_ID });
        }


        public async ValueTask<IEnumerable<LikesQuery>> GetAllLikes()
        {
            string query = @" SELECT * FROM Likes";

            return await _context.Connection.QueryAsync<LikesQuery>(query,
                  new { });
        }

        public async ValueTask<bool> RegisterLike(Like like)
        {
            int rows = 0;

            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    string query = @" INSERT INTO Likes VALUES (@like_ID,@liked,@user_ID,@article_ID) ";

                    rows = await _context.Connection.ExecuteAsync(query,
                          like, transaction);

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

        public async ValueTask<bool> DeleteLike(DeleteLikeCommand deleteCommandLike)
        {
            int rows = 0;

            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    string query = @" DELETE FROM Likes  WHERE User_ID = @user_ID  and Article_ID = @article_ID";

                    rows = await _context.Connection.ExecuteAsync(query,
                          deleteCommandLike, transaction);

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
