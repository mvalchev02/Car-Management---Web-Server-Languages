using Car_Management.Data.Entities;

namespace Car_Management.Data.Repositories.Interfaces
{
    public interface IMaintenanceRepository
    {
        IEnumerable<Maintenance> GetAllMaintenances(int? carId, int? garageId, DateTime? startDate, DateTime? endDate);
        Maintenance GetMaintenanceById(int id);
        void AddMaintenance(Maintenance maintenance);
        void UpdateMaintenance(Maintenance maintenance);
        void DeleteMaintenance(int id);
        IEnumerable<(string Month, int RequestsCount)> GetMonthlyRequestsReport(int? garageId, DateTime startDate, DateTime endDate);
    }
}
