using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using PostDomain.Model;

namespace PostInfrastructure.Services
{
    public class CityImportService : IImportService<City>
    {
        private readonly PostDbContext _context;

        public CityImportService(PostDbContext context)
        {
            _context = context;
        }

        public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanRead)
            {
                throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));
            }

            using (XLWorkbook workBook = new XLWorkbook(stream))
            {
                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                {
                    var cityName = worksheet.Name;
                    var firstDataRow = worksheet.RowsUsed().Skip(1).FirstOrDefault();
                    string countryName = "Не вказано";
                    if (firstDataRow != null && !string.IsNullOrWhiteSpace(firstDataRow.Cell(6).Value.ToString()))
                    {
                        countryName = firstDataRow.Cell(6).Value.ToString();
                    }
                    var city = await _context.Cities.FirstOrDefaultAsync(c => c.Name == cityName, cancellationToken);
                    if (city == null)
                    {
                        city = new City();
                        city.Name = cityName;
                        city.Country = countryName;
                        _context.Cities.Add(city);
                    }
                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        await AddBranchAsync(row, cancellationToken, city);
                    }
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task AddBranchAsync(IXLRow row, CancellationToken cancellationToken, City city)
        {
            string phone = row.Cell(1).Value.ToString();
            string workingHours = row.Cell(2).Value.ToString();
            string street = row.Cell(3).Value.ToString();
            string building = row.Cell(4).Value.ToString();
            string postalCode = row.Cell(5).Value.ToString();
            var location = await _context.BranchLocations.FirstOrDefaultAsync(l =>
                l.City.Id == city.Id &&
                l.Street == street &&
                l.Building == building,
                cancellationToken);
            if (location == null)
            {
                location = new BranchLocation();
                location.City = city;
                location.Street = street;
                location.Building = building;
                location.PostalCode = postalCode;
            }
            var branch = await _context.Branches.FirstOrDefaultAsync(b => b.Phone == phone, cancellationToken);
            if (branch == null)
            {
                branch = new Branch();
                branch.Phone = phone;
                branch.WorkingHours = workingHours;
                branch.Location = location;

                _context.Branches.Add(branch);
            }
            else
            {
                branch.WorkingHours = workingHours;
                branch.Location = location;
            }
        }
    }
}