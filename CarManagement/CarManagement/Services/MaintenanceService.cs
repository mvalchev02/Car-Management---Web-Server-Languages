using Car_Management.Data.Entities;
using Car_Management.Data.Repositories.Interfaces;
using Car_Management.DTOs;
using Car_Management.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car_Management.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly ICarRepository _carRepository;
        private readonly IGarageRepository _garageRepository;

        public MaintenanceService(
            IMaintenanceRepository maintenanceRepository,
            ICarRepository carRepository,
            IGarageRepository garageRepository)
        {
            _maintenanceRepository = maintenanceRepository;
            _carRepository = carRepository;
            _garageRepository = garageRepository;
        }

        public IEnumerable<MaintenanceDTO> GetAllMaintenances(int? carId, int? garageId, DateTime? startDate, DateTime? endDate)
        {
            var maintenances = _maintenanceRepository.GetAllMaintenances(carId, garageId, startDate, endDate);

            return maintenances.Select(m => new MaintenanceDTO
            {
                Id = m.Id,
                CarId = m.CarId,
                CarName = $"{m.Car.Make} {m.Car.Model}",
                ServiceType = m.ServiceType,
                ScheduledDate = m.ScheduledDate,
                GarageId = m.Garage.Id,
                GarageName = m.Garage.Name
            });
        }


        public MaintenanceDTO GetMaintenanceById(int id)
        {
            var maintenance = _maintenanceRepository.GetMaintenanceById(id);
            if (maintenance == null) throw new Exception("Maintenance not found");

            return new MaintenanceDTO
            {
                Id = maintenance.Id,
                CarId = maintenance.Car.Id,
                CarName = maintenance.Car.Make + " " + maintenance.Car.Model,
                ServiceType = maintenance.ServiceType,
                ScheduledDate = maintenance.ScheduledDate,
                GarageId = maintenance.Garage.Id,
                GarageName = maintenance.Garage.Name
            };
        }

        public void AddMaintenance(CreateMaintenanceDto maintenanceDto)
        {
            var car = _carRepository.GetCarById(maintenanceDto.CarId);
            var garage = _garageRepository.GetGarageById(maintenanceDto.GarageId);

            if (car == null) throw new Exception("Car not found");
            if (garage == null) throw new Exception("Garage not found");

            var maintenance = new Maintenance
            {
                Car = car,
                Garage = garage,
                ServiceType = maintenanceDto.ServiceType,
                ScheduledDate = maintenanceDto.ScheduledDate,
                CarName = $"{car.Make} {car.Model}",
                GarageName = ""
            };

            _maintenanceRepository.AddMaintenance(maintenance);
        }

        public void UpdateMaintenance(UpdateMaintenanceDto maintenanceDto)
        {
            var maintenance = _maintenanceRepository.GetMaintenanceById(maintenanceDto.Id);
            if (maintenance == null) throw new Exception("Maintenance not found");

            var car = _carRepository.GetCarById(maintenanceDto.CarId);
            var garage = _garageRepository.GetGarageById(maintenanceDto.GarageId);

            if (car == null) throw new Exception("Car not found");
            if (garage == null) throw new Exception("Garage not found");

            maintenance.Car = car;
            maintenance.Garage = garage;
            maintenance.ServiceType = maintenanceDto.ServiceType;
            maintenance.ScheduledDate = maintenanceDto.ScheduledDate;

            _maintenanceRepository.UpdateMaintenance(maintenance);
        }

        public void DeleteMaintenance(int id)
        {
            _maintenanceRepository.DeleteMaintenance(id);
        }

        public IEnumerable<MonthlyRequestsReportDTO> GetMonthlyRequestsReport(int? garageId, DateTime startDate, DateTime endDate)
        {
            var report = _maintenanceRepository.GetMonthlyRequestsReport(garageId, startDate, endDate);

            return report.Select(r => new MonthlyRequestsReportDTO
            {
                Month = r.Month,
                RequestsCount = r.RequestsCount
            });
        }

    }
}
