using Article.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Article.Domain.Commands.User
{
    public class UpdateUserCommand : Notifiable, ICommand
    {
        public Guid User_ID { get; set; }
        public string Name { get; set; }
        public string Middle_Name { get; set; }
        public void Validate()
        {
            AddNotifications(
           new Contract()
               .Requires()
                .IsNotNull(User_ID, "User_ID", "This value is not valid!")
               .IsNotNull(Name, "Name", "This value is not valid!")
               .IsNotNull(Middle_Name, "Middle_Name", "This value is not valid!")
                );

        }
    }
}
