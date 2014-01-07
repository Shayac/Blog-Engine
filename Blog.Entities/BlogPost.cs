using System;

namespace Blog.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string PostContent { get; set; }
        public DateTime Date { get; set; }
    }
}