using Article.Domain.Commands.Like;
using Article.Domain.Entities;
using Article.Domain.Queries;
using Article.Domain.Queries.Like;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Article.Domain.Repositories
{
    public interface ILikeRepository
    {
        ValueTask<GetQuantityLikeQuery> GetQuantityLikesByArticle(Guid article_ID);
        ValueTask<IEnumerable<LikesQuery>> GetAllLikes();
        ValueTask<bool> RegisterLike(Like like);
        ValueTask<bool> DeleteLike(DeleteLikeCommand deleteCommandLike);
    }
}
