using System;

namespace Article.Domain.Queries.Like
{
    public class LikesQuery
    {
        public Guid Like_Id { get; set; }
        public int Liked { get; set; }
        public Guid User_Id { get; set; }
        public Guid Article_Id { get; set; }
    }
}
