using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
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

        // GET: BlogsController
        public ActionResult Index()
        {
            List<Blog> blogs = _dbContext.Blogs.Where(blog => blog.Status == "Accepted")
                .Include(blog => blog.User)
                .Include(blog => blog.BlogCategory)
                .ToList();
            return View(blogs);
        }

        // GET: BlogsController/Details/5
        public ActionResult Details(int id)
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
            blog.Comments = _dbContext.Comments.Where(comment => comment.BlogId == blog.Id)
                .Include(comment => comment.User).ToList();

            return View(blog);
        }

        [Authenticated]
        // GET: BlogsController/Create
        public ActionResult Create()
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            SelectList categoryOptions = new(_dbContext.BlogCategories, nameof(BlogCategory.Id), nameof(BlogCategory.Name));
            ViewBag.CategoryOptions = categoryOptions;
            return View(new Blog() { UserId = (int)userId! });
        }

        // POST: BlogsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, IFormFile thumbnailFile)
        {
            blog.User = _dbContext.Users.Find(blog.UserId)!;
            if (blog.BlogCategoryId == 0)
            {
                blog.BlogCategoryId = 8;
            }
            blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            if (thumbnailFile != null)
            {
                using MemoryStream memoryStream = new();
                thumbnailFile.CopyTo(memoryStream);
                blog.Thumbnail = memoryStream.ToArray();
            }
            blog.Status = "Pending";
            _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Blog submitted!");
            TempData.Add("success", "Please wait for a moderator to approve your blog.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comment comment)
        {
            if (comment.UserId == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            comment.User = _dbContext.Users.Find(comment.UserId)!;
            comment.Blog = _dbContext.Blogs.Find(comment.BlogId)!;
            comment.CreatedDate = DateTime.Now;
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = comment.BlogId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(Comment comment)
        {
            Comment? commentInDb = _dbContext.Comments.Find(comment.Id);
            if (commentInDb == null)
            {
                TempData.Add("notification", "Comment not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            commentInDb.Contents = comment.Contents;
            commentInDb.ModifiedDate = DateTime.Now;
            _dbContext.Comments.Update(commentInDb);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = comment.BlogId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment(int id)
        {
            Comment? comment = _dbContext.Comments.Find(id);
            if (comment == null)
            {
                TempData.Add("notification", "Comment not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = comment.BlogId });
        }
    }
}
