using Car_Management.Data.Entities;
using Car_Management.Data.Repositories;
using Car_Management.Data.Repositories.Interfaces;
using Car_Management.DTOs;
using Car_Management.Services.Interfaces;

namespace Car_Management.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IGarageRepository _garageRepository;

        public CarService(ICarRepository carRepository, IGarageRepository garageRepository)
        {
            _carRepository = carRepository;
            _garageRepository = garageRepository;
        }

        public IEnumerable<CarDTO> GetAllCars(string make, int? fromYear, int? toYear, int? garageId)
        {
            var cars = _carRepository.GetAllCars(make, fromYear, toYear, garageId);
            return cars.Select(c => new CarDTO
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                ProductionYear = c.ProductionYear,
                LicensePlate = c.LicensePlate,
                Garages = c.Garages.Select(g => g.Id).ToList()
            });
        }


        public CarDTO GetCarById(int id)
        {
            var car = _carRepository.GetCarById(id);
            if (car == null) throw new Exception("Car not found");

            return new CarDTO
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                LicensePlate = car.LicensePlate,
                Garages = car.Garages.Select(g => g.Id).ToList()
            };
        }

        public void AddCar(CreateCarDto carDto)
        {
             if (carDto.GarageIds == null || !carDto.GarageIds.Any())
                throw new Exception("At least one GarageId is required.");

             var garages = carDto.GarageIds.Select(id => _garageRepository.GetGarageById(id)).ToList();

            if (garages.Any(g => g == null))
                throw new Exception("One or more GarageIds are invalid.");

             var car = new Car
            {
                Make = carDto.Make,
                Model = carDto.Model,
                ProductionYear = carDto.ProductionYear,
                LicensePlate = carDto.LicensePlate,
                Garages = garages
            };

            _carRepository.AddCar(car);
        }


        public void UpdateCar(UpdateCarDto carDto)
        {
            var car = _carRepository.GetCarById(carDto.Id);
            if (car == null) throw new Exception("Car not found");

            car.Make = carDto.Make;
            car.Model = carDto.Model;
            car.ProductionYear = carDto.ProductionYear;
            car.LicensePlate = carDto.LicensePlate;
            car.Garages = carDto.GarageIds.Select(id => new Garage { Id = id }).ToList();

            _carRepository.UpdateCar(car);
        }

        public void DeleteCar(int id)
        {
            _carRepository.DeleteCar(id);
        }
    }
}
