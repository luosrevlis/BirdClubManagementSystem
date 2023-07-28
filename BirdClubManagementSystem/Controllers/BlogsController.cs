using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            // if thumbnail is empty return default thumbnail
            if (blog.Thumbnail.Length == 0)
            {
                return File("/img/placeholder/blog.png", "image/png");
            }
            return File(blog.Thumbnail, "image/png");
        }

        public IActionResult Index()
        {
            List<Blog> blogs = _dbContext.Blogs
                .Include(blog => blog.User)
                .Include(blog => blog.BlogCategory)
                .OrderByDescending(blog => blog.DateCreated)
                .ToList();
            return View(blogs);
        }

        public IActionResult Details(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null)
            {
                TempData.Add("notification", "Blog not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
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
            if (blog.BlogCategoryId == 0)
            {
                blog.BlogCategoryId = 7;
            }
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

            TempData.Add("notification", "Blog created!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null)
            {
                TempData.Add("notification", "Blog not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            _dbContext.Blogs.Remove(blog);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Blog deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accept(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                TempData.Add("notification", "Blog not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            blog.Status = "Accepted";
            _dbContext.Blogs.Update(blog);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Blog accepted!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                TempData.Add("notification", "Blog not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            blog.Status = "Rejected";
            _dbContext.Blogs.Update(blog);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Blog rejected!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
