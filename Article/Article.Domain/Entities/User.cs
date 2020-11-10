using System;

namespace Article.Domain.Entities
{
    public class User
    {
        public User(string name, string middle_Name)
        {
            User_ID = Guid.NewGuid();
            Name = name;
            Middle_Name = middle_Name;
        }

        public Guid User_ID { get; set; }
        public string Name { get; set; }
        public string Middle_Name { get; set; }
    }
}
