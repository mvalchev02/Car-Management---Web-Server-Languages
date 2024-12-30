using Car_Management.Data.Entities;
using Car_Management.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car_Management.Data.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly AppDbContext _context;

        public MaintenanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Maintenance> GetAllMaintenances(int? carId, int? garageId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Maintenances
                .Include(m => m.Car)
                .Include(m => m.Garage)
                .AsQueryable();

            if (carId.HasValue)
                query = query.Where(m => m.CarId == carId.Value);

            if (garageId.HasValue)
                query = query.Where(m => m.GarageId == garageId.Value);

            if (startDate.HasValue)
                query = query.Where(m => m.ScheduledDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(m => m.ScheduledDate <= endDate.Value);

            return query.ToList();
        }


        public Maintenance GetMaintenanceById(int id)
        {
            return _context.Maintenances
                .Include(m => m.Car)
                .Include(m => m.Garage)
                .FirstOrDefault(m => m.Id == id);
        }

        public void AddMaintenance(Maintenance maintenance)
        {
            _context.Maintenances.Add(maintenance);
            _context.SaveChanges();
        }

        public void UpdateMaintenance(Maintenance maintenance)
        {
            _context.Maintenances.Update(maintenance);
            _context.SaveChanges();
        }

        public void DeleteMaintenance(int id)
        {
            var maintenance = GetMaintenanceById(id);
            if (maintenance != null)
            {
                _context.Maintenances.Remove(maintenance);
                _context.SaveChanges();
            }
        }

        public IEnumerable<(string Month, int RequestsCount)> GetMonthlyRequestsReport(int? garageId, DateTime startDate, DateTime endDate)
        {
            var query = _context.Maintenances
                .Where(m => m.ScheduledDate >= startDate && m.ScheduledDate <= endDate);

            if (garageId.HasValue)
            {
                query = query.Where(m => m.GarageId == garageId.Value);
            }

            var results = query
                .GroupBy(m => new { m.ScheduledDate.Year, m.ScheduledDate.Month })
                .Select(g => new
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}", 
                    RequestsCount = g.Count()
                })
                .ToList();

            var allMonths = Enumerable.Range(0, (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month + 1)
                .Select(offset => new DateTime(startDate.Year, startDate.Month, 1).AddMonths(offset))
                .Select(date => $"{date.Year}-{date.Month:D2}");

            var report = allMonths
                .GroupJoin(
                    results,
                    month => month,
                    result => result.Month,
                    (month, result) => new
                    {
                        Month = month,
                        RequestsCount = result.FirstOrDefault()?.RequestsCount ?? 0
                    });

            return report.Select(r => (r.Month, r.RequestsCount));
        }

    }
}
