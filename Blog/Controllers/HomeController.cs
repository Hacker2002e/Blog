using Blog.Models;
using Blog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Home
        public ActionResult Index()
        {
            var model = db.Blogs.ToList();
            return View(model);
        }
       
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult ToDoList()
        {
            return View(db.Blogs.ToList());
        }
        [HttpGet]
        public ActionResult Yeni()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Yeni(Blog.Models.EntityFramework.Blog blog)
        {
            if (blog.ID == 0)// insert ucun
            {
                db.Blogs.Add(blog);
               
                db.SaveChanges();
            }
            else
            {
                var updateData = db.Blogs.Find(blog.ID);
                if (updateData == null)
                {
                    return HttpNotFound();
                }
                updateData.Task = blog.Task;
            }
            db.SaveChanges();
            return RedirectToAction("ToDoList", "Home");
        }
        public ActionResult Update(int id)
        {
            var model = db.Blogs.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
           
            return RedirectToAction("Yeni", "Home");
            
        }

        public ActionResult Delete(int id)
        {
            var delete = db.Blogs.Find(id);
            if (delete == null)
            {
                return HttpNotFound();
            }
            db.Blogs.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("ToDoList", "Home");
        }

    }
}
    