using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using PostDomain.Model;

namespace PostInfrastructure.Services
{
    public class CityExportService : IExportService<City>
    {
        private readonly PostDbContext _context;

        public CityExportService(PostDbContext context)
        {
            _context = context;
        }

        public async Task WriteToAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanWrite)
            {
                throw new ArgumentException("Потік не доступний для запису");
            }
            var cities = await _context.Cities
                .Include(c => c.BranchLocations)
                    .ThenInclude(l => l.Branches)
                .ToListAsync(cancellationToken);

            var workbook = new XLWorkbook();

            foreach (var city in cities)
            {
                if (city != null)
                {
                    var worksheet = workbook.Worksheets.Add(city.Name);
                    worksheet.Cell(1, 1).Value = "Телефон";
                    worksheet.Cell(1, 2).Value = "Години роботи";
                    worksheet.Cell(1, 3).Value = "Вулиця";
                    worksheet.Cell(1, 4).Value = "Будинок";
                    worksheet.Cell(1, 5).Value = "Індекс";
                    worksheet.Cell(1, 6).Value = "Країна";
                    worksheet.Row(1).Style.Font.Bold = true;

                    int rowIndex = 2;
                    foreach (var location in city.BranchLocations)
                    {
                        foreach (var branch in location.Branches)
                        {
                            worksheet.Cell(rowIndex, 1).Value = branch.Phone;
                            worksheet.Cell(rowIndex, 2).Value = branch.WorkingHours;
                            worksheet.Cell(rowIndex, 3).Value = location.Street;
                            worksheet.Cell(rowIndex, 4).Value = location.Building;
                            worksheet.Cell(rowIndex, 5).Value = location.PostalCode;
                            worksheet.Cell(rowIndex, 6).Value = city.Country;

                            rowIndex++;
                        }
                    }
                }
            }
            workbook.SaveAs(stream);
        }
    }
}
