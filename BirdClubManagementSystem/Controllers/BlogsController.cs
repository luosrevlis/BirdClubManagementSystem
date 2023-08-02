using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Statuses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class BlogsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

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

        public IActionResult Index(int page = 1, string keyword = "", string status = "")
        {
            IQueryable<Blog> matches = _dbContext.Blogs;
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(blog => blog.Title.ToLower().Contains(keyword.ToLower()));
            }
            if (!string.IsNullOrEmpty(status))
            {
                matches = matches.Where(blog => blog.Status == status);
            }

            int maxPage = (int)Math.Ceiling(matches.Count() / (double)PageSize);
            if (page > maxPage)
            {
                page = maxPage;
            }
            if (page < 1)
            {
                page = 1;
            }

            List<BlogDTO> blogs = matches
                .OrderByDescending(blog => blog.DateCreated)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Include(blog => blog.User)
                .Include(blog => blog.BlogCategory)
                .Select(blog => _mapper.Map<BlogDTO>(blog))
                .ToList();

            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.Status = status;
            ViewBag.MaxPage = maxPage;
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
            return View(_mapper.Map<BlogDTO>(blog));
        }

        public IActionResult Create()
        {
            SelectList categoryOptions = new(
                _dbContext.BlogCategories,
                nameof(BlogCategory.Id),
                nameof(BlogCategory.Name));
            ViewBag.CategoryOptions = categoryOptions;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogDTO dto, IFormFile thumbnailFile)
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
            blog.Status = BlogStatuses.Accepted;
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
            if (blog == null || blog.Status != BlogStatuses.Pending)
            {
                TempData.Add("notification", "Blog not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            blog.Status = BlogStatuses.Accepted;
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
            if (blog == null || blog.Status != BlogStatuses.Pending)
            {
                TempData.Add("notification", "Blog not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            blog.Status = BlogStatuses.Rejected;
            _dbContext.Blogs.Update(blog);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Blog rejected!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
