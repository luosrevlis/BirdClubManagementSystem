using Microsoft.AspNetCore.Mvc;
using BirdClubInfoHub.Data;
using BirdClubInfoHub.Models.Entities;
using AutoMapper;
using BirdClubInfoHub.Models.DTOs;

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

        public IActionResult Index(int page = 1)
        {
            List<IClubEventDTO> eventList = new();
            eventList.AddRange(_dbContext.FieldTrips
                .Select(e => _mapper.Map<FieldTripDTO>(e))
                .Cast<IClubEventDTO>());
            eventList.AddRange(_dbContext.Meetings
                .Select(e => _mapper.Map<MeetingDTO>(e))
                .Cast<IClubEventDTO>());
            eventList.AddRange(_dbContext.Tournaments
                .Select(e => _mapper.Map<TournamentDTO>(e))
                .Cast<IClubEventDTO>());
            eventList.Sort((e1, e2) => -e1.StartDate
                .CompareTo(e2.StartDate));
            return View(eventList
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }
    }
}
