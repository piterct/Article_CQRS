﻿using Article.Domain.Commands.Article;
using Article.Domain.Entities;
using Article.Domain.Queries.Article;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Article.Domain.Repositories
{
    public interface IArticleRepository
    {
        ValueTask<ArticleQuery> GetArticle(Guid article_ID);
        ValueTask<IEnumerable<ArticleQuery>> GetAllArticles();
        ValueTask<bool> RegisterArticle(Articles article);
        ValueTask<bool> UpdateArticle(UpdateArticleCommand article);
        ValueTask<bool> DeleteArticle(DeleteArticleCommand article);
    }
}
