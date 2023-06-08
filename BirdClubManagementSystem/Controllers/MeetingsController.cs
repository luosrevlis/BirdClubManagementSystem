using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public MeetingsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: MeetingsController
        public IActionResult Index()
        {
            return View();
        }

        // GET: MeetingsController/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // GET: MeetingsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Meetings.Add(meeting);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(meeting);
        }

        // GET: MeetingsController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: MeetingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Meetings.Update(meeting);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(meeting);
        }

        // GET: MeetingController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        // POST: MeetingController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return NotFound();
            }
            _dbContext.Meetings.Remove(meeting);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "ClubEvents");
        }
    }
}
