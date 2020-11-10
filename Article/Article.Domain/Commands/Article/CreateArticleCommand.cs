using Article.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace Article.Domain.Commands.Article
{
    public class CreateArticleCommand : Notifiable, ICommand
    {
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
