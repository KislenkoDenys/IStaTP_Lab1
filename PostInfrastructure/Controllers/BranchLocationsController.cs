using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PostDomain.Model;
using PostInfrastructure;

namespace PostInfrastructure.Controllers
{
    public class BranchLocationsController : Controller
    {
        private readonly PostDbContext _context;

        public BranchLocationsController(PostDbContext context)
        {
            _context = context;
        }

        // GET: BranchLocations
        public async Task<IActionResult> Index(int ?id, string? name)
        {
            if (id == 0) return RedirectToAction("Cities", "Index");
            ViewBag.CityId = id;
            ViewBag.CityName = name;
            var branchLocationsByCity = _context.BranchLocations.Where(b => b.CityId == id).Include(b => b.City);
            return View(await branchLocationsByCity.ToListAsync());
        }

        // GET: BranchLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchLocation = await _context.BranchLocations
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branchLocation == null)
            {
                return NotFound();
            }

            return View(branchLocation);
        }

        // GET: BranchLocations/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Country");
            return View();
        }

        // POST: BranchLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,Street,Building,PostalCode,Id")] BranchLocation branchLocation)
        {
            City city = _context.Cities.FirstOrDefault(c => c.Id == branchLocation.CityId);
            branchLocation.City = city;
            ModelState.Clear();
            TryValidateModel(branchLocation);

            if (ModelState.IsValid)
            {
                _context.Add(branchLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Country", branchLocation.CityId);
            return View(branchLocation);
        }

        // GET: BranchLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchLocation = await _context.BranchLocations.FindAsync(id);
            if (branchLocation == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", branchLocation.CityId);
            return View(branchLocation);
        }

        // POST: BranchLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,Street,Building,PostalCode,Id")] BranchLocation branchLocation)
        {
            if (id != branchLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branchLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchLocationExists(branchLocation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { branchLocation.CityId });
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", branchLocation.CityId);
            return View(branchLocation);
        }

        // GET: BranchLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchLocation = await _context.BranchLocations
                .Include(b => b.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (branchLocation == null)
            {
                return NotFound();
            }

            return View(branchLocation);
        }

        // POST: BranchLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branchLocation = await _context.BranchLocations.FindAsync(id);
            if (branchLocation != null)
            {
                _context.BranchLocations.Remove(branchLocation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { branchLocation.CityId });
        }

        private bool BranchLocationExists(int id)
        {
            return _context.BranchLocations.Any(e => e.Id == id);
        }
    }
}
