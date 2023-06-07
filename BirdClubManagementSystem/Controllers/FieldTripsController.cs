using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BirdClubManagementSystem.Data;
using BirdClubManagementSystem.Models;

namespace BirdClubManagementSystem.Controllers
{
    public class FieldTripsController : Controller
    {
        private readonly BcmsDbContext _context;

        public FieldTripsController(BcmsDbContext context)
        {
            _context = context;
        }

        // GET: FieldTrips
        public async Task<IActionResult> Index()
        {
              return _context.FieldTrips != null ? 
                          View(await _context.FieldTrips.ToListAsync()) :
                          Problem("Entity set 'BcmsDbContext.FieldTrips'  is null.");
        }

        // GET: FieldTrips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FieldTrips == null)
            {
                return NotFound();
            }

            var fieldTrip = await _context.FieldTrips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fieldTrip == null)
            {
                return NotFound();
            }

            return View(fieldTrip);
        }

        // GET: FieldTrips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FieldTrips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Description,Fee,IsAvailable")] FieldTrip fieldTrip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fieldTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fieldTrip);
        }

        // GET: FieldTrips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FieldTrips == null)
            {
                return NotFound();
            }

            var fieldTrip = await _context.FieldTrips.FindAsync(id);
            if (fieldTrip == null)
            {
                return NotFound();
            }
            return View(fieldTrip);
        }

        // POST: FieldTrips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Description,Fee,IsAvailable")] FieldTrip fieldTrip)
        {
            if (id != fieldTrip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fieldTrip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldTripExists(fieldTrip.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fieldTrip);
        }

        // GET: FieldTrips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FieldTrips == null)
            {
                return NotFound();
            }

            var fieldTrip = await _context.FieldTrips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fieldTrip == null)
            {
                return NotFound();
            }

            return View(fieldTrip);
        }

        // POST: FieldTrips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FieldTrips == null)
            {
                return Problem("Entity set 'BcmsDbContext.FieldTrips'  is null.");
            }
            var fieldTrip = await _context.FieldTrips.FindAsync(id);
            if (fieldTrip != null)
            {
                _context.FieldTrips.Remove(fieldTrip);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FieldTripExists(int id)
        {
          return (_context.FieldTrips?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
