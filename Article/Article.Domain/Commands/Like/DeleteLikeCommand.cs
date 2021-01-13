using Article.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Article.Domain.Commands.Like
{
    public class DeleteLikeCommand : Notifiable, ICommand
    {
        public Guid User_ID { get; set; }
        public Guid Article_ID { get; set; }

        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNull(User_ID, "User_ID", "This value is not valid!")
               .IsNotNull(Article_ID, "Article_ID", "This value is not valid!")
                );
        }
    }
}
