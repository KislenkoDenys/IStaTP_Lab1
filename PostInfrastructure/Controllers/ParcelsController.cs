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
    public class ParcelsController : Controller
    {
        private readonly PostDbContext _context;

        public ParcelsController(PostDbContext context)
        {
            _context = context;
        }

        // GET: Parcels
        public async Task<IActionResult> Index()
        {
            var postDbContext = _context.Parcels.Include(p => p.DeliveryCity).Include(p => p.Receiver).Include(p => p.ReceiverBranch).Include(p => p.Sender).Include(p => p.SenderBranch).Include(p => p.Tariff);
            return View(await postDbContext.ToListAsync());
        }

        // GET: Parcels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcels
                .Include(p => p.DeliveryCity)
                .Include(p => p.Receiver)
                .Include(p => p.ReceiverBranch)
                .Include(p => p.Sender)
                .Include(p => p.SenderBranch)
                .Include(p => p.Tariff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }

            return View(parcel);
        }

        // GET: Parcels/Create
        public IActionResult Create()
        {
            ViewData["DeliveryCityId"] = new SelectList(_context.Cities, "Id", "Country");
            ViewData["ReceiverId"] = new SelectList(_context.Customers, "Id", "Adress");
            ViewData["ReceiverBranchId"] = new SelectList(_context.Branches, "Id", "Phone");
            ViewData["SenderId"] = new SelectList(_context.Customers, "Id", "Adress");
            ViewData["SenderBranchId"] = new SelectList(_context.Branches, "Id", "Phone");
            ViewData["TariffId"] = new SelectList(_context.Tariffs, "Id", "Name");
            return View();
        }

        // POST: Parcels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SenderId,ReceiverId,SenderBranchId,ReceiverBranchId,DeliveryCityId,DeliveryAddress,TariffId,Weight,Status,Id")] Parcel parcel)
        {
            parcel.Status = ParcelStatus.Created;
            var sender = _context.Customers.FirstOrDefault(c => c.Id == parcel.SenderId);
            var receiver = _context.Customers.FirstOrDefault(c => c.Id == parcel.ReceiverId);
            var senderBranch = _context.Branches.Include(b => b.Location).ThenInclude(l => l.City).FirstOrDefault(b => b.Id == parcel.SenderBranchId);
            var receiverBranch = _context.Branches.Include(b => b.Location).ThenInclude(l => l.City).FirstOrDefault(b => b.Id == parcel.ReceiverBranchId);
            var deliveryCity = _context.Cities.FirstOrDefault(c => c.Id == parcel.DeliveryCityId);
            var tariff = _context.Tariffs.FirstOrDefault(t => t.Id == parcel.TariffId);
            parcel.Sender = sender;
            parcel.Receiver = receiver;
            parcel.SenderBranch = senderBranch;
            parcel.ReceiverBranch = receiverBranch;
            parcel.DeliveryCity = deliveryCity;
            parcel.Tariff = tariff;
            ModelState.Clear();
            TryValidateModel(parcel);
            if (ModelState.IsValid)
            {
                _context.Add(parcel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliveryCityId"] = new SelectList(_context.Cities, "Id", "Country", parcel.DeliveryCityId);
            ViewData["ReceiverId"] = new SelectList(_context.Customers, "Id", "Adress", parcel.ReceiverId);
            ViewData["ReceiverBranchId"] = new SelectList(_context.Branches, "Id", "Phone", parcel.ReceiverBranchId);
            ViewData["SenderId"] = new SelectList(_context.Customers, "Id", "Adress", parcel.SenderId);
            ViewData["SenderBranchId"] = new SelectList(_context.Branches, "Id", "Phone", parcel.SenderBranchId);
            ViewData["TariffId"] = new SelectList(_context.Tariffs, "Id", "Name", parcel.TariffId);
            return View(parcel);
        }

        // GET: Parcels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcels.FindAsync(id);
            if (parcel == null)
            {
                return NotFound();
            }
            ViewData["DeliveryCityId"] = new SelectList(_context.Cities, "Id", "Country", parcel.DeliveryCityId);
            ViewData["ReceiverId"] = new SelectList(_context.Customers, "Id", "Adress", parcel.ReceiverId);
            ViewData["ReceiverBranchId"] = new SelectList(_context.Branches, "Id", "Phone", parcel.ReceiverBranchId);
            ViewData["SenderId"] = new SelectList(_context.Customers, "Id", "Adress", parcel.SenderId);
            ViewData["SenderBranchId"] = new SelectList(_context.Branches, "Id", "Phone", parcel.SenderBranchId);
            ViewData["TariffId"] = new SelectList(_context.Tariffs, "Id", "Name", parcel.TariffId);
            return View(parcel);
        }

        // POST: Parcels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SenderId,ReceiverId,SenderBranchId,ReceiverBranchId,DeliveryCityId,DeliveryAddress,TariffId,Weight,Status,Id")] Parcel parcel)
        {
            if (id != parcel.Id)
            {
                return NotFound();
            }
            var sender = _context.Customers.FirstOrDefault(c => c.Id == parcel.SenderId);
            var receiver = _context.Customers.FirstOrDefault(c => c.Id == parcel.ReceiverId);
            var senderBranch = _context.Branches.Include(b => b.Location).ThenInclude(l => l.City).FirstOrDefault(b => b.Id == parcel.SenderBranchId);
            var receiverBranch = _context.Branches.Include(b => b.Location).ThenInclude(l => l.City).FirstOrDefault(b => b.Id == parcel.ReceiverBranchId);
            var deliveryCity = _context.Cities.FirstOrDefault(c => c.Id == parcel.DeliveryCityId);
            var tariff = _context.Tariffs.FirstOrDefault(t => t.Id == parcel.TariffId);
            parcel.Sender = sender;
            parcel.Receiver = receiver;
            parcel.SenderBranch = senderBranch;
            parcel.ReceiverBranch = receiverBranch;
            parcel.DeliveryCity = deliveryCity;
            parcel.Tariff = tariff;
            ModelState.Clear();
            TryValidateModel(parcel);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcelExists(parcel.Id))
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
            ViewData["DeliveryCityId"] = new SelectList(_context.Cities, "Id", "Country", parcel.DeliveryCityId);
            ViewData["ReceiverId"] = new SelectList(_context.Customers, "Id", "Adress", parcel.ReceiverId);
            ViewData["ReceiverBranchId"] = new SelectList(_context.Branches, "Id", "Phone", parcel.ReceiverBranchId);
            ViewData["SenderId"] = new SelectList(_context.Customers, "Id", "Adress", parcel.SenderId);
            ViewData["SenderBranchId"] = new SelectList(_context.Branches, "Id", "Phone", parcel.SenderBranchId);
            ViewData["TariffId"] = new SelectList(_context.Tariffs, "Id", "Name", parcel.TariffId);
            return View(parcel);
        }

        // GET: Parcels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcel = await _context.Parcels
                .Include(p => p.DeliveryCity)
                .Include(p => p.Receiver)
                .Include(p => p.ReceiverBranch)
                .Include(p => p.Sender)
                .Include(p => p.SenderBranch)
                .Include(p => p.Tariff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcel == null)
            {
                return NotFound();
            }

            return View(parcel);
        }

        // POST: Parcels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcel = await _context.Parcels.FindAsync(id);
            if (parcel != null)
            {
                _context.Parcels.Remove(parcel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcelExists(int id)
        {
            return _context.Parcels.Any(e => e.Id == id);
        }
    }
}
