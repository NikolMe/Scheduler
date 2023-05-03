using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scheduler.Data;
using Scheduler.Models;

namespace Scheduler.Controllers
{
    [Authorize]
    public class PlacesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Places
        public async Task<IActionResult> Index(string procedure)
        {
            var places = _context.Place.Include(p => p.Service).AsQueryable();
            if (string.IsNullOrEmpty(procedure))
            {
                
            }
            else if (procedure == "Без запису")
            {
                places = places.Where(p => p.ServiceId == null);
            }
            else
            {
                places = places.Where(p => p.Service.Procedure == procedure);
            }

            var procedures = await _context.Service.Select(p => p.Procedure).ToListAsync();
            procedures.Add("Без запису");

            var result = new FilterPlacesViewModel()
            {
                Places = places.ToList(),
                Procedures = new SelectList(procedures)
            };
            return View(result);
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // GET: Places/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.Set<Service>(), "Id", "Id");
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Time,IsSelected,ClientName,ClientNumber,ServiceId,EndOfTheDay")]
            Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ServiceId"] = new SelectList(_context.Set<Service>(), "Id", "Id", place.ServiceId);
            return View(place);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            ViewData["ServiceId"] = new SelectList(_context.Set<Service>(), "Id", "Id", place.ServiceId);
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Time,IsSelected,ClientName,ClientNumber,ServiceId,EndOfTheDay")]
            Place place)
        {
            if (id != place.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.Id))
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

            ViewData["ServiceId"] = new SelectList(_context.Set<Service>(), "Id", "Id", place.ServiceId);
            return View(place);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place
                .Include(p => p.Service)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Place == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Place'  is null.");
            }

            var place = await _context.Place.FindAsync(id);
            if (place != null)
            {
                _context.Place.Remove(place);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SelfPlace(int placeId, int serviceId, DateTime date)
        {
            return View(new SelfPlaceViewModel
            {
                PlaceId = placeId,
                ServiceId = serviceId, 
                Date = date
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SelfPlace(int placeId, string name, string phone, int serviceId)
        {
            var place = await _context.Place.FirstAsync(p => p.Id == placeId);

            place.ClientName = name;
            place.ClientNumber = phone;
            place.IsSelected = true;
            place.ServiceId = serviceId;

            var currentService = await _context.Service.FirstAsync(x => x.Id == serviceId);
            var deleteList = _context.Place
                .Include(x => x.Service)
                .Where(p => p.Time > place.Time && place.Time.AddMinutes(currentService.TimeStamp) > p.Time && p.IsSelected == false);

            _context.Place.RemoveRange(deleteList);
            _context.Place.Update(place);

            await _context.SaveChangesAsync();

            TempData["success"] = "Ваш запис успішно створено!";
            return RedirectToAction("Index", "Home");
        }

        private bool PlaceExists(int id)
        {
            return _context.Place.Any(e => e.Id == id);
        }
    }
}