using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Blog.Entities;

namespace Blog.Models
{
    public class AllBlogPostsViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public string ChopOffPost(BlogPost post)
        {
            post.PostBody = Regex.Replace(post.PostBody, "<.*?>", "");
            if (post.PostBody.Length < 100)
            {
                return post.PostBody;
            }
            int lastSpace = post.PostBody.LastIndexOf(" ", 100);
            return post.PostBody.Substring(0, (lastSpace > 0) ? lastSpace : 100);
            
            
        }
    }
}   