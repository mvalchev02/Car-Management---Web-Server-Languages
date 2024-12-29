using Car_Management.Data.Entities;
using Car_Management.DTOs;

namespace Car_Management.Data.Repositories.Interfaces
{
    public interface IGarageRepository
    {
        IEnumerable<Garage> GetAllGaragesByCity(string cityName);
        Garage GetGarageById(int id);
        void AddGarage(Garage garage);
        void UpdateGarage(Garage garage);
        void DeleteGarage(int id);
        IEnumerable<GarageDailyAvailabilityReportDTO> GetDailyAvailabilityReport(int garageId, DateTime startDate, DateTime endDate);
    }
}
