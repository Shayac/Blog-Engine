using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Entities;

namespace Blog.Models
{
    public class BlogViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
    }
}