using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class BlogsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public BlogsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult GetImageFromBytes(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            // if thumbnail is empty return default thumbnail, 5 places
            return File(blog.Thumbnail, "image/png");
        }

        public IActionResult Index()
        {
            List<Blog> blogs = _dbContext.Blogs.ToList();
            foreach (Blog blog in blogs)
            {
                blog.User = _dbContext.Users.Find(blog.UserId)!;
                blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            }
            return View(blogs);
        }

        public IActionResult Details(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            blog.User = _dbContext.Users.Find(blog.UserId)!;
            blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            return View(blog);
        }

        public IActionResult Create()
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            SelectList categoryOptions = new(_dbContext.BlogCategories, nameof(BlogCategory.Id), nameof(BlogCategory.Name));
            ViewBag.CategoryOptions = categoryOptions;
            return View(new Blog() { UserId = (int)userId! });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog, IFormFile thumbnailFile)
        {
            blog.User = _dbContext.Users.Find(blog.UserId)!;
            blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            if (thumbnailFile != null)
            {
                using MemoryStream memoryStream = new();
                thumbnailFile.CopyTo(memoryStream);
                blog.Thumbnail = memoryStream.ToArray();
            }
            blog.Status = "Accepted";
            _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
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

        public IActionResult Accept(int id)
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

        [HttpPost, ActionName("Accept")]
        [ValidateAntiForgeryToken]
        public IActionResult AcceptConfirmed(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                return NotFound();
            }
            blog.Status = "Accepted";
            _dbContext.Blogs.Update(blog);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
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

        [HttpPost, ActionName("Reject")]
        [ValidateAntiForgeryToken]
        public IActionResult RejectConfirmed(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                return NotFound();
            }
            blog.Status = "Rejected";
            _dbContext.Blogs.Update(blog);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
