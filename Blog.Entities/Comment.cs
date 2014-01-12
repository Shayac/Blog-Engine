using System;

namespace Blog.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BlogPostId { get; set; }
        public string CommentBody { get; set; }
        public DateTime Date { get; set; }
        public int? ReplyId { get; set; }
    }
}