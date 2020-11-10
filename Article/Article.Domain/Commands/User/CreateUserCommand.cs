using Article.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace Article.Domain.Commands.User
{
    public class CreateUserCommand : Notifiable, ICommand
    {
        public string Name { get; set; }
        public string Middle_Name { get; set; }
        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
               .IsNotNull(Name, "Name", "This value is not valid!")
               .IsNotNull(Middle_Name, "Middle_Name", "This value is not valid!")
                );

        }
    }
}
