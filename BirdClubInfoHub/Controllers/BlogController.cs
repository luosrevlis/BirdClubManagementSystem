using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using BirdClubInfoHub.Data;
using System.Collections;
using BirdClubInfoHub.Models;

namespace BirdClubInfoHub.Controllers
{
    public class BlogController : Controller
    {
        private readonly BcmsDbContext _db;
        public BlogController(BcmsDbContext db)
        {
            _db = db;
        }

        // GET: BlogController
        public ActionResult Index()
        {
            IEnumerable<BlogCategory> objBCateList = _db.BlogCategories;
            return View(objBCateList);
        }

        // GET: BlogController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var blogcate = _db.Blogs.FirstOrDefault(c => c.BlogCategoryId == id);
            if (blogcate == null)
            {
                return NotFound();
            }
            return View(blogcate);
        }

        // GET: BlogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
