﻿using AutoMapper;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubInfoHub.Controllers
{
    public class ClubEventsController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public ClubEventsController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index(DateTime month = new DateTime(), int page = 1, string keyword = "", string status = "")
        {
            if (month.Ticks < 1)
            {
                month = DateTime.Now;
            }
            List<IClubEventDTO> eventList = new();
            eventList.AddRange(_dbContext.FieldTrips
                .Where(e => e.StartDate.Month == month.Month && e.StartDate.Year == month.Year)
                .Select(e => _mapper.Map<FieldTripDTO>(e))
                .Cast<IClubEventDTO>());
            eventList.AddRange(_dbContext.Meetings
                .Where(e => e.StartDate.Month == month.Month && e.StartDate.Year == month.Year)
                .Select(e => _mapper.Map<MeetingDTO>(e))
                .Cast<IClubEventDTO>());
            eventList.AddRange(_dbContext.Tournaments
                .Where(e => e.StartDate.Month == month.Month && e.StartDate.Year == month.Year)
                .Select(e => _mapper.Map<TournamentDTO>(e))
                .Cast<IClubEventDTO>());

            if (!string.IsNullOrEmpty(keyword))
            {
                eventList = eventList.Where(e => e.Name.ToLower().Contains(keyword.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                eventList = eventList.Where(e => e.Status == status).ToList();
            }

            int maxPage = (int)Math.Ceiling(eventList.Count / (double)PageSize);
            if (page > maxPage)
            {
                page = maxPage;
            }
            if (page < 1)
            {
                page = 1;
            }

            eventList = eventList
                .OrderByDescending(e => e.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.Month = month;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            ViewBag.Status = status;
            ViewBag.MaxPage = maxPage;
            return View(eventList);
        }
    }
}
