using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class BlogListController : Controller
    {
        // GET: BlogListController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BlogListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BlogListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogListController/Create
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

        // GET: BlogListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogListController/Edit/5
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

        // GET: BlogListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BlogListController/Delete/5
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
