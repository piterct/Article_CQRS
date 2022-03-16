using Article.Domain.Commands.Article;
using Article.Domain.Entities;
using Article.Domain.Repositories;
using Article.Shared.Commands;
using Article.Shared.Commands.Contracts;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Article.Domain.Handlers
{
    public class ArticleHandler : Notifiable
    {
        private readonly IArticleRepository _repository;

        public ArticleHandler(IArticleRepository repository)
        {
            _repository = repository;

        }
        public async ValueTask<ICommandResult> Handle(CreateArticleCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);
            }

            var article = new Articles(command.Name);

            bool done = await _repository.RegisterArticle(article);

            if (done)
                return new GenericCommandResult(true, "Article Registered!", article, StatusCodes.Status201Created, null);


            return new GenericCommandResult(true, "Internal Error!", null, StatusCodes.Status500InternalServerError, null);
        }

        public async ValueTask<ICommandResult> Handle(UpdateArticleCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var article = await _repository.GetArticle(command.Article_ID);

            if (article == null)
            {
                return new GenericCommandResult(false, "User  Not Found!", null, StatusCodes.Status404NotFound, command.Notifications);
            }
            else
            {
                bool updated = await _repository.UpdateArticle(command);

                if (updated)
                    return new GenericCommandResult(true, "Article Updated!", article, StatusCodes.Status200OK, null);


                return new GenericCommandResult(updated, "Internal Server Error!", null, StatusCodes.Status500InternalServerError, null);
            }
        }

        public async ValueTask<ICommandResult> Handle(DeleteArticleCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);

            bool deleted = await _repository.DeleteArticle(command);

            if (deleted)
                return new GenericCommandResult(true, "Deleted Article!", null, StatusCodes.Status200OK, null);


            return new GenericCommandResult(deleted, "Internal Error!", null, StatusCodes.Status500InternalServerError, null);
        }
    }
}
