using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Entities;

namespace Blog.Models
{
    public class AllBlogPostsViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public string ChopOffPost(BlogPost post)
        {
            int lastSpace = post.PostContent.LastIndexOf(" ", 100);
            return post.PostContent.Substring(0, (lastSpace > 0) ? lastSpace : 100);
        }
    }
}