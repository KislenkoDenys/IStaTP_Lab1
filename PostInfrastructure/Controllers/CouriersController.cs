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
    public class CouriersController : Controller
    {
        private readonly PostDbContext _context;

        public CouriersController(PostDbContext context)
        {
            _context = context;
        }

        // GET: Couriers
        public async Task<IActionResult> Index()
        {
            var postDbContext = _context.Couriers.Include(c => c.Branch).Include(c => c.VehicleType);
            return View(await postDbContext.ToListAsync());
        }

        // GET: Couriers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers
                .Include(c => c.Branch)
                .Include(c => c.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // GET: Couriers/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Phone");
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name");
            return View();
        }

        // POST: Couriers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,Surname,VehicleTypeId,PhoneNumber,IsAvailable,BranchId,Id")] Courier courier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Phone", courier.BranchId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", courier.VehicleTypeId);
            return View(courier);
        }

        // GET: Couriers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers.FindAsync(id);
            if (courier == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Phone", courier.BranchId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", courier.VehicleTypeId);
            return View(courier);
        }

        // POST: Couriers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,Surname,VehicleTypeId,PhoneNumber,IsAvailable,BranchId,Id")] Courier courier)
        {
            if (id != courier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourierExists(courier.Id))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Phone", courier.BranchId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", courier.VehicleTypeId);
            return View(courier);
        }

        // GET: Couriers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _context.Couriers
                .Include(c => c.Branch)
                .Include(c => c.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // POST: Couriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courier = await _context.Couriers.FindAsync(id);
            if (courier != null)
            {
                _context.Couriers.Remove(courier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourierExists(int id)
        {
            return _context.Couriers.Any(e => e.Id == id);
        }
    }
}
