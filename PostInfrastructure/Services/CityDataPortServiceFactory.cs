using PostDomain.Model;

namespace PostInfrastructure.Services
{
    public class CityDataPortServiceFactory : IDataPortServiceFactory<City>
    {
        private readonly PostDbContext _context;

        public CityDataPortServiceFactory(PostDbContext context)
        {
            _context = context;
        }

        public IImportService<City> GetImportService(string contentType)
        {
            if (contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new CityImportService(_context);
            }
            throw new NotImplementedException($"Немає сервісу імпорту для типу {contentType}");
        }

        public IExportService<City> GetExportService(string contentType)
        {
            if (contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new CityExportService(_context);
            }
            throw new NotImplementedException($"Немає сервісу експорту для типу {contentType}");
        }
    }
}