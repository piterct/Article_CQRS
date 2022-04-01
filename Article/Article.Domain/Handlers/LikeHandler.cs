using Article.Domain.Commands.Like;
using Article.Domain.Entities;
using Article.Domain.Enums;
using Article.Domain.Handlers.Interfaces;
using Article.Domain.Repositories;
using Article.Shared.Commands;
using Article.Shared.Commands.Contracts;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Article.Domain.Handlers
{
    public class LikeHandler : Notifiable, ILikeHandler

    {

        private readonly ILikeRepository _repository;

        public LikeHandler(ILikeRepository repository)
        {
            _repository = repository;

        }
        public async ValueTask<ICommandResult> Handle(AddLikeArticleCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var like = new Like((int)ELikeType.Like, command.User_ID, command.Article_ID);

            bool done = await _repository.RegisterLike(like);

            if (done)
                return new GenericCommandResult(true, "Like Registered!", like, StatusCodes.Status201Created, null);


            return new GenericCommandResult(done, "Internal Error!", null, StatusCodes.Status500InternalServerError, null);
        }

        public async ValueTask<ICommandResult> Handle(DeleteLikeCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var done = await _repository.DeleteLike(command);

            if (done)
                return new GenericCommandResult(true, "Like deleted!", null, StatusCodes.Status200OK, null);

            return new GenericCommandResult(done, "Internal Error!", null, StatusCodes.Status500InternalServerError, null);
        }
    }
}
