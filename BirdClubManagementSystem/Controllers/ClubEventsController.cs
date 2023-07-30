using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [StaffAuthenticated]
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

        public IActionResult Index(int page = 1, string keyword = "")
        {
            List<IClubEventDTO> eventList = new();
            eventList.AddRange(_dbContext.FieldTrips
                .Where(e => e.Name.ToLower().Contains(keyword.ToLower()))
                .Select(e => _mapper.Map<FieldTripDTO>(e))
                .Cast<IClubEventDTO>());
            eventList.AddRange(_dbContext.Meetings
                .Where(e => e.Name.ToLower().Contains(keyword.ToLower()))
                .Select(e => _mapper.Map<MeetingDTO>(e))
                .Cast<IClubEventDTO>());
            eventList.AddRange(_dbContext.Tournaments
                .Where(e => e.Name.ToLower().Contains(keyword.ToLower()))
                .Select(e => _mapper.Map<TournamentDTO>(e))
                .Cast<IClubEventDTO>());
            eventList = eventList
                .OrderByDescending(e => e.StartDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            return View(eventList);
        }
    }
}
