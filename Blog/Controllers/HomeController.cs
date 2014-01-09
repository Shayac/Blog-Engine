using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Data;
using Blog.Entities;
using Blog.Models;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private BlogDB db = new BlogDB(ConfigurationManager.AppSettings["BlogPostConnectionString"]);

        public ActionResult Index()
        {

            IEnumerable<BlogPost> posts = db.GetBlogPosts();
            AllBlogPostsViewModel model = new AllBlogPostsViewModel(){BlogPosts = posts};
            return View(model);
        }

        public ActionResult Post(int id)
        {
            BlogPost post = db.GetBlogPost(id);
            BlogPostViewModel model = new BlogPostViewModel() {Post = post};
            IEnumerable<Comment> comments = db.GetComments(id);
            CommentViewModel commentViewModel = new CommentViewModel() {Comments = comments};
            
            return View(model);
        }
    }
}
