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
            // if thumbnail is empty return default thumbnail, 4 places
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
                return NotFound();
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
            blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            using (MemoryStream memoryStream = new())
            {
                thumbnailFile.CopyTo(memoryStream);
                blog.Thumbnail = memoryStream.ToArray();
            }
            blog.Status = "Pending";
            _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
