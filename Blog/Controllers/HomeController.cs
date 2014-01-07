using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Data;
using Blog.Entities;

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
            return View();
        }

    }
}
