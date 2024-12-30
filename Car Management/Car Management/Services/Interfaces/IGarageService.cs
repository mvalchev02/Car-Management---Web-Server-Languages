using Car_Management.DTOs;

namespace Car_Management.Services.Interfaces
{
    public interface IGarageService
    {
        IEnumerable<GarageDto> GetAllGaragesByCity(string city);
        GarageDto GetGarageById(int id);
        void AddGarage(GarageDto garageDto);
        void UpdateGarage(GarageDto garageDto);
        void DeleteGarage(int id);
        IEnumerable<GarageDailyAvailabilityReportDTO> GetDailyAvailabilityReport(int garageId, DateTime startDate, DateTime endDate);
    }
}
