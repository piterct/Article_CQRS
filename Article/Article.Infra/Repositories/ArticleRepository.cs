using Article.Domain.Commands.Article;
using Article.Domain.Entities;
using Article.Domain.Queries.Article;
using Article.Domain.Repositories;
using Article.Infra.DataContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Article.Infra.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleDataContext _context;
        public ArticleRepository(ArticleDataContext context)
        {
            _context = context;
        }
        public async ValueTask<ArticleQuery> GetArticle(Guid article_ID)
        {

            string query = @" SELECT * FROM Articles                              
                               WHERE Article_ID = @article_ID ";

            return await _context.Connection.QuerySingleOrDefaultAsync<ArticleQuery>(query,
                  new { Article_ID = article_ID });

        }

        public async ValueTask<IEnumerable<ArticleQuery>> GetAllArticles()
        {
            string query = @" SELECT * FROM Articles";

            return await _context.Connection.QueryAsync<ArticleQuery>(query,
                  new { });
        }

        public async ValueTask<bool> RegisterArticle(Articles article)
        {
            int rows = 0;

            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    string query = @" INSERT INTO Articles VALUES (@article_ID,@Name) ";

                    rows = await _context.Connection.ExecuteAsync(query,
                          article, transaction);

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

        public async ValueTask<bool> UpdateArticle(UpdateArticleCommand article)
        {
            int rows = 0;

            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    string query = @" UPDATE Articles SET Name = @Name  WHERE Article_ID = @article_ID   ";

                    rows = await _context.Connection.ExecuteAsync(query,
                          article, transaction);

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

        public async ValueTask<bool> DeleteArticle(DeleteArticleCommand article)
        {
            int rows = 0;

            using (var transaction = _context.Connection.BeginTransaction())
            {
                try
                {
                    string query = @" DELETE FROM Articles  WHERE Article_ID = @article_ID ";

                    rows = await _context.Connection.ExecuteAsync(query,
                          article, transaction);

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
