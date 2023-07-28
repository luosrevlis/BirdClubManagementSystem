using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class MeetingsController : Controller
    {
        private readonly BcmsDbContext _dbContext;

        public MeetingsController(BcmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: MeetingsController/Details/5
        public IActionResult Details(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
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
            if (meeting.StartDate < meeting.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(meeting);
            }
            meeting.Status = "Open";
            _dbContext.Meetings.Add(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", meeting.Name + " has been created!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // GET: MeetingsController/Edit/5
        public IActionResult Edit(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(meeting);
        }

        // POST: MeetingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Meeting meeting)
        {
            if (meeting.StartDate < meeting.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(meeting);
            }
            Meeting? meetingInDb = _dbContext.Meetings.Find(meeting.Id);
            if (meetingInDb == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            meetingInDb.Name = meeting.Name;
            meetingInDb.StartDate = meeting.StartDate;
            meetingInDb.RegCloseDate = meeting.RegCloseDate;
            meetingInDb.Description = meeting.Description;
            _dbContext.Meetings.Update(meetingInDb);
            _dbContext.SaveChanges();

            TempData.Add("notification", meetingInDb.Name + " has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: MeetingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.Meetings.Remove(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", meeting.Name + " has been deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: MeetingController/Close/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Close(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            meeting.Status = "Registration Closed";
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Registration for " + meeting.Name + " has been closed!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: MeetingController/MarkAsEnded/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAsEnded(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            meeting.Status = "Ended";
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", meeting.Name + " has been marked as ended!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: MeetingController/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            meeting.Status = "Cancelled";
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();

            TempData.Add("notification", meeting.Name + " has been cancelled!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        public IActionResult EditHighlights(int id)
        {
            Meeting? meeting = _dbContext.Meetings.Find(id);
            if (meeting == null || meeting.Status != "Ended")
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(meeting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHighlights(Meeting meeting)
        {
            Meeting? meetingInDb = _dbContext.Meetings.Find(meeting.Id);
            if (meetingInDb == null || meetingInDb.Status != "Ended")
            {
                TempData.Add("notification", "Meeting not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            meetingInDb.Highlights = meeting.Highlights;
            _dbContext.Meetings.Update(meetingInDb);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Highlights has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Details", new { id = meeting.Id });
        }
    }
}
