using Car_Management.Data.Entities;
using Car_Management.Data.Repositories.Interfaces;
using Car_Management.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Car_Management.Data.Repositories
{
    public class GarageRepository : IGarageRepository
    {
        private readonly AppDbContext _context;

        public GarageRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Garage> GetAllGaragesByCity(string city = null)
        {
            return string.IsNullOrEmpty(city)
                ? _context.Garages.ToList()
                : _context.Garages.Where(g => g.City == city).ToList();
        }

        public Garage GetGarageById(int id)
        {
            return _context.Garages.FirstOrDefault(g => g.Id == id);
        }

        public void AddGarage(Garage garage)
        {
            _context.Garages.Add(garage);
            _context.SaveChanges();
        }

        public void UpdateGarage(Garage garage)
        {
            _context.Garages.Update(garage);
            _context.SaveChanges();
        }

        public void DeleteGarage(int id)
        {
            var garage = GetGarageById(id);
            if (garage != null)
            {
                _context.Garages.Remove(garage);
                _context.SaveChanges();
            }
        }
        public IEnumerable<GarageDailyAvailabilityReportDTO> GetDailyAvailabilityReport(int garageId, DateTime startDate, DateTime endDate)
        {
            var results = new List<GarageDailyAvailabilityReportDTO>();

            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                var totalCapacity = _context.Garages
                    .Where(g => g.Id == garageId)
                    .Select(g => g.Capacity)
                    .FirstOrDefault();

                var requestsCount = _context.Maintenances
                    .Where(m => m.GarageId == garageId && m.ScheduledDate.Date == date)
                    .Count();

                results.Add(new GarageDailyAvailabilityReportDTO
                {
                    Date = date.ToString("yyyy-MM-dd"),
                    Requests = requestsCount,
                    AvailableCapacity = totalCapacity - requestsCount
                });
            }

            return results;
        }


    }
}
