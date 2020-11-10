using Article.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Article.Domain.Commands.User
{
    public class DeleteUserCommand : Notifiable, ICommand
    {
        public Guid User_ID { get; set; }
        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNull(User_ID, "User_ID", "Minimum 3 characters!")
                );
        }
    }
}
