using Article.Domain.Handlers;
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
            services.AddTransient<UserHandler, UserHandler>();
            services.AddTransient<LikeHandler, LikeHandler>();
            services.AddTransient<ArticleHandler, ArticleHandler>();
            #endregion

            #region Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<IArticleRepository, ArticleRepository>();

            #endregion
        }
    }
}
