using Car_Management.Data.Entities;
using Car_Management.Data.Repositories.Interfaces;
using Car_Management.DTOs;
using Car_Management.Services.Interfaces;

namespace Car_Management.Services
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;

        public GarageService(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }

        public IEnumerable<GarageDto> GetAllGaragesByCity(string city)
        {
            var garages = _garageRepository.GetAllGaragesByCity(city);
            return garages.Select(g => new GarageDto
            {
                Id = g.Id,
                Name = g.Name,
                Location = g.Location,
                City = g.City,
                Capacity = g.Capacity
            });
        }

        public GarageDto GetGarageById(int id)
        {
            var garage = _garageRepository.GetGarageById(id);
            if (garage == null) throw new Exception("Garage not found");
            return new GarageDto
            {
                Id = garage.Id,
                Name = garage.Name,
                Location = garage.Location,
                City = garage.City,
                Capacity = garage.Capacity
            };
        }

        public void AddGarage(GarageDto garageDto)
        {
             if (garageDto.Id > 0)
            {
                var existingGarage = _garageRepository.GetGarageById(garageDto.Id);
                if (existingGarage == null)
                    throw new Exception($"Garage with ID {garageDto.Id} not found.");

                 garageDto.Name = existingGarage.Name;
                garageDto.Location = existingGarage.Location;
                garageDto.City = existingGarage.City;
                garageDto.Capacity = existingGarage.Capacity;
            }

            var garage = new Garage
            {
                Name = garageDto.Name,
                Location = garageDto.Location,
                City = garageDto.City,
                Capacity = garageDto.Capacity
            };

            _garageRepository.AddGarage(garage);
        }


        public void UpdateGarage(GarageDto garageDto)
        {
            var garage = _garageRepository.GetGarageById(garageDto.Id);
            if (garage == null) throw new Exception("Garage not found");

            garage.Name = garageDto.Name;
            garage.Location = garageDto.Location;
            garage.City = garageDto.City;
            garage.Capacity = garageDto.Capacity;

            _garageRepository.UpdateGarage(garage);
        }

        public void DeleteGarage(int id)
        {
            _garageRepository.DeleteGarage(id);
        }
        public IEnumerable<GarageDailyAvailabilityReportDTO> GetDailyAvailabilityReport(int garageId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("startDate must be before or equal to endDate.");

            return _garageRepository.GetDailyAvailabilityReport(garageId, startDate, endDate);
        }


    }
}
