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
    public class ParcelDimensionsController : Controller
    {
        private readonly PostDbContext _context;

        public ParcelDimensionsController(PostDbContext context)
        {
            _context = context;
        }

        // GET: ParcelDimensions
        public async Task<IActionResult> Index()
        {
            var postDbContext = _context.ParcelDimensions.Include(p => p.Parcel);
            return View(await postDbContext.ToListAsync());
        }

        // GET: ParcelDimensions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcelDimension = await _context.ParcelDimensions
                .Include(p => p.Parcel)
                .FirstOrDefaultAsync(m => m.ParcelId == id);
            if (parcelDimension == null)
            {
                return NotFound();
            }

            return View(parcelDimension);
        }

        // GET: ParcelDimensions/Create
        public IActionResult Create()
        {
            ViewData["ParcelId"] = new SelectList(_context.Parcels, "Id", "Id");
            return View();
        }

        // POST: ParcelDimensions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParcelId,LengthCm,WidthCm,HeightCm")] ParcelDimension parcelDimension)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcelDimension);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcelId"] = new SelectList(_context.Parcels, "Id", "Id", parcelDimension.ParcelId);
            return View(parcelDimension);
        }

        // GET: ParcelDimensions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcelDimension = await _context.ParcelDimensions.FindAsync(id);
            if (parcelDimension == null)
            {
                return NotFound();
            }
            ViewData["ParcelId"] = new SelectList(_context.Parcels, "Id", "Id", parcelDimension.ParcelId);
            return View(parcelDimension);
        }

        // POST: ParcelDimensions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParcelId,LengthCm,WidthCm,HeightCm")] ParcelDimension parcelDimension)
        {
            if (id != parcelDimension.ParcelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcelDimension);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcelDimensionExists(parcelDimension.ParcelId))
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
            ViewData["ParcelId"] = new SelectList(_context.Parcels, "Id", "Id", parcelDimension.ParcelId);
            return View(parcelDimension);
        }

        // GET: ParcelDimensions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcelDimension = await _context.ParcelDimensions
                .Include(p => p.Parcel)
                .FirstOrDefaultAsync(m => m.ParcelId == id);
            if (parcelDimension == null)
            {
                return NotFound();
            }

            return View(parcelDimension);
        }

        // POST: ParcelDimensions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcelDimension = await _context.ParcelDimensions.FindAsync(id);
            if (parcelDimension != null)
            {
                _context.ParcelDimensions.Remove(parcelDimension);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcelDimensionExists(int id)
        {
            return _context.ParcelDimensions.Any(e => e.ParcelId == id);
        }
    }
}
