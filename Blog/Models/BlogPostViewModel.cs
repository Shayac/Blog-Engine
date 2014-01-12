using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Entities;

namespace Blog.Models
{
    public class BlogPostViewModel
    {
        public BlogPost Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<User> Users { get; set; } 
    }
}