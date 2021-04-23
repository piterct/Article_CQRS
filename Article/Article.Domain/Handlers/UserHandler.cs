using Article.Domain.Commands.User;
using Article.Domain.Entities;
using Article.Domain.Repositories;
using Article.Shared.Commands;
using Article.Shared.Commands.Contracts;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Article.Domain.Handlers
{
    public class UserHandler : Notifiable
    {
        private readonly IUserRepository _repository;

        public UserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<ICommandResult> Handle(CreateUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var user = new User(command.Name, command.Middle_Name);

            bool done = await _repository.RegisterUser(user);

            if (done)
                return new GenericCommandResult(true, "User Registered!", user, StatusCodes.Status201Created, null);


            return new GenericCommandResult(done, "Internal Error!", null, StatusCodes.Status500InternalServerError, null);
        }

        public async ValueTask<ICommandResult> Handle(UpdateUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);

            var user = await _repository.GetUser(command.User_ID);

            if (user == null)
            {
                return new GenericCommandResult(false, "User  Not Found!", null, StatusCodes.Status404NotFound, command.Notifications);
            }
            else
            {
                bool updated = await _repository.UpdateUser(command);

                if (updated)
                    return new GenericCommandResult(true, "User Updated!", user, StatusCodes.Status200OK, null);


                return new GenericCommandResult(updated, "Internal Error!", null, StatusCodes.Status500InternalServerError, null);
            }
        }

        public async ValueTask<ICommandResult> Handle(DeleteUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Incorrect  data!", null, StatusCodes.Status400BadRequest, command.Notifications);

            bool deleted = await _repository.DeleteUser(command);

            if (deleted)
                return new GenericCommandResult(true, "User Deleted!", null, StatusCodes.Status200OK, null);


            return new GenericCommandResult(deleted, "Internal Error!", null, StatusCodes.Status500InternalServerError, null);
        }
    }
}
