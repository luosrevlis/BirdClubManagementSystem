﻿using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using BirdClubManagementSystem.Models.Statuses;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
    public class TournamentsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public TournamentsController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1, string keyword = "", string status = "")
        {
            return Index(DateTime.Now, page, keyword, status);
        }

        public IActionResult Index(DateTime month, int page = 1, string keyword = "", string status = "")
        {
            IQueryable<Tournament> matches = _dbContext.Tournaments
                .Where(t => t.StartDate.Month == month.Month && t.StartDate.Year == month.Year);
            if (!string.IsNullOrEmpty(status))
            {
                matches = matches.Where(t => t.Status == status);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches.Where(t => t.Name.ToLower().Contains(keyword.ToLower()));
            }

            List<TournamentDTO> tournaments = matches
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .OrderByDescending(t => t.StartDate)
                .Select(t => _mapper.Map<TournamentDTO>(t))
                .ToList();
            return View(tournaments);
        }

        // GET: TournamentsController/Details/5
        public IActionResult Details(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(_mapper.Map<TournamentDTO>(tournament));
        }

        // GET: TournamentsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TournamentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TournamentDTO dto)
        {
            if (dto.StartDate < dto.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(dto);
            }
            Tournament tournament = _mapper.Map<Tournament>(dto);
            
            if (tournament.RegOpenDate < DateTime.Now && tournament.RegCloseDate > DateTime.Now)
            {
                tournament.Status = EventStatuses.RegOpened;
            }
            else
            {
                tournament.Status = EventStatuses.RegClosed;
            }
            _dbContext.Tournaments.Add(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", tournament.Name + " has been created!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // GET: TournamentsController/Edit/5
        public IActionResult Edit(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(_mapper.Map<TournamentDTO>(tournament));
        }

        // POST: TournamentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TournamentDTO dto)
        {
            if (dto.StartDate < dto.RegCloseDate)
            {
                TempData.Add("notification", "Date error!");
                TempData.Add("error", "Event cannot take place before registration is closed!");
                return View(dto);
            }
            Tournament? tournament = _dbContext.Tournaments.Find(dto.Id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            tournament.Name = dto.Name;
            tournament.RegOpenDate = dto.RegOpenDate;
            tournament.RegCloseDate = tournament.RegCloseDate;
            tournament.StartDate = dto.StartDate;
            tournament.ExpectedEndDate = dto.ExpectedEndDate;
            tournament.Address = dto.Address;
            tournament.RegLimit = dto.RegLimit;
            tournament.Description = tournament.Description;
            tournament.Fee = dto.Fee;
            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", tournament.Name + " has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        // POST: TournamentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            _dbContext.Tournaments.Remove(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", tournament.Name + " has been deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, string statusCode)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null)
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            tournament.Status = statusCode;
            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Status updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index", "ClubEvents");
        }

        public IActionResult EditHighlights(int id)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(id);
            if (tournament == null || tournament.Status != "Ended")
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            return View(_mapper.Map<TournamentDTO>(tournament));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHighlights(TournamentDTO dto)
        {
            Tournament? tournament = _dbContext.Tournaments.Find(dto.Id);
            if (tournament == null || tournament.Status != "Ended")
            {
                TempData.Add("notification", "Tournament not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index", "ClubEvents");
            }
            tournament.Highlights = dto.Highlights;
            _dbContext.Tournaments.Update(tournament);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Highlights has been updated!");
            TempData.Add("success", "");
            return RedirectToAction("Details", new { id = tournament.Id });
        }
    }
}
