﻿using BirdClubInfoHub.Data;
using BirdClubInfoHub.Filters;
using BirdClubInfoHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BirdClubInfoHub.Controllers
{
    [Authenticated]
    public class BlogHistoryController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public BlogHistoryController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            int userId = (int)HttpContext.Session.GetInt32("USER_ID")!;
            List<Blog> createdBlogs = _dbContext.Blogs.Where(blog => blog.UserId == userId)
                .Include(blog => blog.User)
                .Include(blog => blog.BlogCategory)
                .ToList();
            return View(createdBlogs);
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
            if (blog.Status == "Accepted")
            {
                return RedirectToAction("Details", "Blogs", new { id });
            }
            return View(blog);
        }

        public IActionResult Edit(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog blog, IFormFile thumbnailFile)
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

        public IActionResult Delete(int id)
        {
            Blog? blog = _dbContext.Blogs.Find(id);
            if (blog == null || blog.Status != "Pending")
            {
                return NotFound();
            }
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
    }
}