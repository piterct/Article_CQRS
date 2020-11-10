using System;

namespace Article.Domain.Entities
{
    public class Like
    {

        public Like(int liked, Guid user_ID, Guid article_ID)
        {
            Like_ID = Guid.NewGuid();
            Liked = liked;
            User_ID = user_ID;
            Article_ID = article_ID;
        }

        public Guid Like_ID { get; set; }
        public int Liked { get; set; }
        public Guid User_ID { get; set; }
        public Guid Article_ID { get; set; }
    }
}
