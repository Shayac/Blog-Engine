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
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private BlogDB db = new BlogDB(ConfigurationManager.AppSettings["BlogPostConnectionString"]);
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewPost()
        {
            IEnumerable<Author> authors = db.GetAuthors();
            AuthorsViewModel model = new AuthorsViewModel(){Authors = authors};
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreatePost(BlogPost post)
        {
            post.Date = DateTime.Now;
            int postid = db.CreatePost(post);

            return RedirectToAction("Index");
        }
    }
}
