using Car_Management.DTOs;

namespace Car_Management.Services.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarDTO> GetAllCars(string make, int? fromYear, int? toYear, int? garageId);
        CarDTO GetCarById(int id);
        void AddCar(CreateCarDto carDto);
        void UpdateCar(UpdateCarDto carDto);
        void DeleteCar(int id);
    }
}
