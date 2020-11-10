using Article.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Article.Domain.Commands.Article
{
    public class DeleteArticleCommand : Notifiable, ICommand
    {
        public Guid Article_ID { get; set; }

        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNull(Article_ID, "Article_ID", "This value is not valid!")
                );
        }
    }
}
