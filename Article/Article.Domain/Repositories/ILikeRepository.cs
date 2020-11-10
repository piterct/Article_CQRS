using Article.Domain.Entities;
using Article.Domain.Queries;
using System;
using System.Threading.Tasks;

namespace Article.Domain.Repositories
{
    public interface ILikeRepository
    {
        ValueTask<GetQuantityLikeQuery> GetQuantityLikesByArticle(Guid article_ID);
        ValueTask<bool> RegisterLike(Like like);
    }
}
