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
    [Authenticated]
    public class BlogHistoryController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public BlogHistoryController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1, string keyword = "", int categoryId = 0)
        {
            int? userId = HttpContext.Session.GetInt32("USER_ID");
            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            IQueryable<Blog> matches = _dbContext.Blogs
                .Where(blog => blog.UserId == userId);
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(blog => blog.Title.ToLower().Contains(keyword.ToLower()));
            }
            if (categoryId != 0)
            {
                matches = matches.Where(blog => blog.BlogCategoryId == categoryId);
            }

            List<BlogDTO> createdBlogs = matches
                .OrderByDescending(blog => blog.DateCreated)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Include(blog => blog.BlogCategory)
                .Select(blog => _mapper.Map<BlogDTO>(blog))
                .ToList();
            return View(createdBlogs);
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
            if (blog.Status == "Accepted")
            {
                return RedirectToAction("Details", "Blogs", new { id });
            }
            blog.User = _dbContext.Users.Find(blog.UserId)!;
            blog.BlogCategory = _dbContext.BlogCategories.Find(blog.BlogCategoryId)!;
            return View(_mapper.Map<BlogDTO>(blog));
        }

        public IActionResult Edit(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != BlogStatuses.Pending)
            {
                TempData.Add("notification", "Blog not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            SelectList categoryOptions = new(
                _dbContext.BlogCategories,
                nameof(BlogCategory.Id),
                nameof(BlogCategory.Name));
            ViewBag.CategoryOptions = categoryOptions;
            return View(_mapper.Map<BlogDTO>(blog));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogDTO dto, IFormFile thumbnailFile)
        {
            Blog? blog = _dbContext.Blogs.Find(dto.Id);
            if (blog == null || blog.Status != BlogStatuses.Pending)
            {
                TempData.Add("notification", "Blog not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            blog.BlogCategory = _dbContext.BlogCategories.Find(dto.BlogCategory.Id)!;
            blog.Title = dto.Title;
            blog.Contents = dto.Contents;
            blog.DateCreated = DateTime.Now;
            if (thumbnailFile != null)
            {
                using MemoryStream memoryStream = new();
                thumbnailFile.CopyTo(memoryStream);
                blog.Thumbnail = memoryStream.ToArray();
            }
            _dbContext.Blogs.Update(blog);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Blog updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != BlogStatuses.Pending)
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
    }
}
