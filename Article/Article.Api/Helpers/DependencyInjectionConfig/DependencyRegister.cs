﻿using Article.Domain.Handlers;
using Article.Domain.Handlers.Interfaces;
using Article.Domain.Repositories;
using Article.Infra.DataContext;
using Article.Infra.Repositories;
using Article.Shared.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Article.Api.Helpers.DependencyInjectionConfig
{
    public static class DependencyRegister
    {
        public static void AddScoped(this IServiceCollection services, IConfiguration configuration)
        {

            #region IOptions
            services.Configure<ArticleSettings>(configuration.GetSection("ConnectionStringArticle"));
            #endregion

            #region Configurations
            services.AddScoped<ArticleDataContext, ArticleDataContext>();
            #endregion

            #region Handlers
            services.AddScoped<UserHandler, UserHandler>();
            services.AddScoped<ILikeHandler, LikeHandler>();
            services.AddScoped<IArticleHandler, ArticleHandler>();
            services.AddScoped<IUserHandler, UserHandler>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            #endregion
        }
    }
}
