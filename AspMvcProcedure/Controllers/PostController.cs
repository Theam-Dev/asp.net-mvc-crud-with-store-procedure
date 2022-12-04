using AspMvcProcedure.Data;
using AspMvcProcedure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspMvcProcedure.Controllers
{
    public class PostController : Controller
    {
        readonly PostServie db = new PostServie();

        [HttpGet]
        public ActionResult Index()
        {
            var data = db.Gets();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Post post)
        {
            try
            {
                if (db.Add(post))
                {
                    ModelState.Clear();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = db.Gets().Find(m => m.Id == id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Post post)
        {
            try
            {
                db.Update(post);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var data = db.Gets().Find(m => m.Id == id);
            return View(data);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                PostServie sdb = new PostServie();
                if (sdb.Delete(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}