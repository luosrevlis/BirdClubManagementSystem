using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models.DTOs;
using BirdClubInfoHub.Models.Entities;
using BirdClubInfoHub.Models.Statuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    public class BlogsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10; //TODO move to config?

        public BlogsController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActionResult GetImageFromBytes(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null)
            {
                return NotFound();
            }
            // if thumbnail is empty return default thumbnail
            if (blog.Thumbnail == null || blog.Thumbnail.Length == 0)
            {
                return File("/img/placeholder/blog.png", "image/png");
            }
            return File(blog.Thumbnail, "image/png");
        }

        // GET: BlogsController
        public ActionResult Index(int page = 1, string keyword = "", int categoryId = 0)
        {
            IQueryable<Blog> matches = _dbContext.Blogs
                .Where(blog => blog.Status == BlogStatuses.Accepted);
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(blog => blog.Title.ToLower().Contains(keyword.ToLower()));
            }
            if (categoryId != 0)
            {
                matches = matches.Where(blog => blog.BlogCategoryId == categoryId);
            }
            
            List<BlogDTO> blogs = matches
                .OrderByDescending(blog => blog.DateCreated)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Include(blog => blog.User)
                .Include(blog => blog.BlogCategory)
                .Select(blog => _mapper.Map<BlogDTO>(blog))
                .ToList();

            SelectList categoryOptions = new(
                _dbContext.BlogCategories,
                nameof(BlogCategory.Id),
                nameof(BlogCategory.Name));
            ViewBag.CategoryOptions = categoryOptions;

            ViewBag.NewBlogs = _dbContext.Blogs
                .Where(blog => blog.Status == BlogStatuses.Accepted)
                .OrderByDescending(blog => blog.DateCreated)
                .Take(3) //TODO move 3 to config or const
                .Select(blog => _mapper.Map<BlogDTO>(blog))
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
            blog.Comments = _dbContext.Comments
                .Where(comment => comment.BlogId == blog.Id)
                .Include(comment => comment.User)
                .ToList();

            SelectList categoryOptions = new(
                _dbContext.BlogCategories,
                nameof(BlogCategory.Id),
                nameof(BlogCategory.Name));
            ViewBag.CategoryOptions = categoryOptions;
            ViewBag.NewBlogs = _dbContext.Blogs
                .Where(blog => blog.Status == BlogStatuses.Accepted)
                .OrderByDescending(blog => blog.DateCreated)
                .Take(3)
                .Select(blog => _mapper.Map<BlogDTO>(blog))
                .ToList();

            return View(_mapper.Map<BlogDTO>(blog));
        }

        [Authenticated]
        // GET: BlogsController/Create
        public ActionResult Create()
        {
            SelectList categoryOptions = new(
                _dbContext.BlogCategories,
                nameof(BlogCategory.Id),
                nameof(BlogCategory.Name));
            ViewBag.CategoryOptions = categoryOptions;
            return View();
        }

        // POST: BlogsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogDTO dto, IFormFile thumbnailFile)
        {
            Blog blog = _mapper.Map<Blog>(dto);
            blog.User = _dbContext.Users.Find(dto.User.Id)!;
            blog.BlogCategory = _dbContext.BlogCategories.Find(dto.BlogCategory.Id)!;
            if (thumbnailFile != null)
            {
                using MemoryStream memoryStream = new();
                thumbnailFile.CopyTo(memoryStream);
                blog.Thumbnail = memoryStream.ToArray();
            }
            blog.Status = BlogStatuses.Pending;
            _dbContext.Blogs.Add(blog);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Blog submitted!");
            TempData.Add("success", "Please wait for a moderator to approve your blog.");
            return RedirectToAction("Index");
        }

        [Authenticated]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(CommentDTO dto)
        {
            if (dto.User.Id == 0)
            {
                return RedirectToAction("Index", "Login");
            }
            Comment comment = _mapper.Map<Comment>(dto);
            comment.User = _dbContext.Users.Find(dto.User.Id)!;
            comment.Blog = _dbContext.Blogs.Find(dto.Blog.Id)!;
            comment.CreatedDate = DateTime.Now;
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = dto.Blog.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(CommentDTO dto)
        {
            Comment? comment = _dbContext.Comments.Find(dto.Id);
            if (comment == null)
            {
                TempData.Add("notification", "Comment not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            comment.Contents = dto.Contents;
            comment.ModifiedDate = DateTime.Now;
            _dbContext.Comments.Update(comment);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = dto.Blog.Id });
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
