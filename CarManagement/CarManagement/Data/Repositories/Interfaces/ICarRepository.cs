using Car_Management.Data.Entities;

namespace Car_Management.Data.Repositories.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars(string make = null, int? garageId = null, int? fromYear = null, int? toYear = null);
        Car GetCarById(int id);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
    }
}
