using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class PendingBlogsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public PendingBlogsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: PendingBlogsController
        public ActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("USER_ID")!;
            List<Blog> pendingBlogs = _dbContext.Blogs.Where(blog => blog.UserId == userId && blog.Status == "Pending").ToList();
            foreach (Blog blog in pendingBlogs)
            {
                blog.User = _dbContext.Users.Find(blog.UserId)!;
                blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            }
            return View(pendingBlogs);
        }

        // GET: PendingBlogsController/Details/5
        public ActionResult Details(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                return NotFound();
            }
            blog.User = _dbContext.Users.Find(blog.UserId)!;
            blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            return View(blog);
        }

        // GET: PendingBlogsController/Edit/5
        public ActionResult Edit(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                return NotFound();
            }
            SelectList categoryOptions = new(_dbContext.BlogCategories, nameof(BlogCategory.Id), nameof(BlogCategory.Name));
            ViewBag.CategoryOptions = categoryOptions;
            return View(blog);
        }

        // POST: PendingBlogsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog, IFormFile thumbnailFile)
        {
            Blog? blogInDb = _dbContext.Blogs.Find(blog.Id);
            if (blogInDb == null || blogInDb.Status != "Pending")
            {
                return NotFound();
            }
            blogInDb.BlogCategoryId = blog.BlogCategoryId;
            blogInDb.Title = blog.Title;
            blogInDb.Contents = blog.Contents;
            if (thumbnailFile != null)
            {
                using MemoryStream memoryStream = new();
                thumbnailFile.CopyTo(memoryStream);
                blogInDb.Thumbnail = memoryStream.ToArray();
            }
            _dbContext.Blogs.Update(blogInDb);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: PendingBlogsController/Delete/5
        public ActionResult Delete(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                return NotFound();
            }
            blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            return View(blog);
        }

        // POST: PendingBlogsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                return NotFound();
            }
            _dbContext.Blogs.Remove(blog);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
