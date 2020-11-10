using System;

namespace Article.Domain.Entities
{
    public class Articles
    {
        public Articles(string name)
        {
            Article_ID = Guid.NewGuid();
            Name = name;
        }
        public Guid Article_ID { get; set; }
        public string Name { get; set; }
    }
}

