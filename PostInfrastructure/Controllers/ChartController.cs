using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostDomain.Model;

namespace PostInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private record CityBranchCount(string City, int Count);
        private record TariffParcelCount(string Tariff, int Count);

        private readonly PostDbContext _context;
        public ChartController(PostDbContext context)
        {
            _context = context;
        }

        [HttpGet("branchesByCity")]
        public async Task<JsonResult> GetBranchesByCityAsync(CancellationToken cancellationToken)
        {
            var data = await _context.Branches.Include(b => b.Location).ThenInclude(l => l.City).GroupBy(b => b.Location.City.Name).Select(g => new CityBranchCount(g.Key, g.Count())).ToListAsync(cancellationToken);

            return new JsonResult(data);
        }
        [HttpGet("parcelsByTariff")]
        public async Task<JsonResult> GetParcelsByTariffAsync(CancellationToken cancellationToken)
        {
            var data = await _context.Parcels
                .Include(p => p.Tariff)
                .GroupBy(p => p.Tariff.Name)
                .Select(g => new TariffParcelCount(g.Key, g.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(data);
        }
    }
}
