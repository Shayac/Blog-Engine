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
            model.Comments = db.GetComments(id);
            model.Users = db.GetUsers();
            
            
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            comment.Date = DateTime.Now;
            db.CreateComment(comment);

            return RedirectToAction("Post/"+comment.BlogPostId);
        }

        public ActionResult NewUser()
        {
            return View();
        }

        public ActionResult CreateUser(User user)
        {
            db.CreateUser(user);
            return RedirectToAction("Index");
        }
    }
}
