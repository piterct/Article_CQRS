using Article.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Article.Domain.Commands.Article
{
    public class UpdateArticleCommand : Notifiable, ICommand
    {
        public Guid Article_ID { get; set; }
        public string Name { get; set; }

        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNull(Name, "Name", "This value is not valid!")
                );
        }
    }
}
