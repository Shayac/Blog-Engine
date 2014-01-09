using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Entities;

namespace Blog.Models
{
    public class CommentViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
    }
}