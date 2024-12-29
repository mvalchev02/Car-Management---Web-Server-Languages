using Car_Management.DTOs;

namespace Car_Management.Services.Interfaces
{
    public interface IMaintenanceService
    {
        IEnumerable<MaintenanceDTO> GetAllMaintenances(int? carId, int? garageId, DateTime? startDate, DateTime? endDate);
        MaintenanceDTO GetMaintenanceById(int id);
        void AddMaintenance(CreateMaintenanceDto maintenanceDto);
        void UpdateMaintenance(UpdateMaintenanceDto maintenanceDto);
        void DeleteMaintenance(int id);
        IEnumerable<MonthlyRequestsReportDTO> GetMonthlyRequestsReport(int? garageId, DateTime startDate, DateTime endDate);

    }
}
