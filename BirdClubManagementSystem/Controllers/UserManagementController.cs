using AutoMapper;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Filters;
using BirdClubManagementSystem.Models.DTOs;
using BirdClubManagementSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubManagementSystem.Controllers
{
    [AdminAuthenticated]
    public class UserManagementController : Controller
    {
        private readonly BcmsDbContext _dbContext;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public UserManagementController(BcmsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: UserManagementController
        public IActionResult Index(int page = 1, string keyword = "", string role = "")
        {
            IQueryable<User> matches = _dbContext.Users;
            if (!string.IsNullOrEmpty(keyword))
            {
                matches = matches
                    .Where(user => user.Name.ToLower().Contains(keyword.ToLower())
                    || user.Email.ToLower().Contains(keyword.ToLower()));
            }
            if (!string.IsNullOrEmpty(role))
            {
                matches = matches.Where(user => user.Role == role);
            }
            
            List<UserDTO> users = matches
                .OrderByDescending(user => user.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(user => _mapper.Map<UserDTO>(user))
                .ToList();
            return View(users);
        }

        // GET: UserManagementController/Details/5
        public IActionResult Details(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            return View(_mapper.Map<UserDTO>(user));
        }

        // GET: UserManagementController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserManagementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserDTO dto)
        {
            User? user = _dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user != null)
            {
                TempData.Add("notification", "Email existed!");
                TempData.Add("error", "An account with the same email already existed!");
                return RedirectToAction("Index");
            }
            user = _mapper.Map<User>(dto);
            PasswordHasher<User> passwordHasher = new();
            user.Password = passwordHasher.HashPassword(user, user.Password);
            user.JoinDate = DateTime.Now;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Account created!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        // GET: UserManagementController/Edit/5
        public IActionResult Edit(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            return View(_mapper.Map<UserDTO>(user));
        }

        // POST: UserManagementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserDTO dto)
        {
            User? user = _dbContext.Users.Find(dto.Id);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            user.Name = dto.Name;
            user.Address = dto.Address;
            user.Phone = dto.Phone;
            user.Role = dto.Role;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Account updated!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }

        // POST: UserManagementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            User? user = _dbContext.Users.Find(id);
            if (user == null)
            {
                TempData.Add("notification", "Account not found!");
                TempData.Add("error", "");
                return RedirectToAction("Index");
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            TempData.Add("notification", "Account deleted!");
            TempData.Add("success", "");
            return RedirectToAction("Index");
        }
    }
}
