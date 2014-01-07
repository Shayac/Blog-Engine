using System;

namespace Blog.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CommentContent { get; set; }
        public DateTime Date { get; set; }
        public int? ReplyId { get; set; }
    }
}