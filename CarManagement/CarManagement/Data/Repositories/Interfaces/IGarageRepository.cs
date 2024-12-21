using Car_Management.Data.Entities;

namespace Car_Management.Data.Repositories.Interfaces
{
    public interface IGarageRepository
    {
        IEnumerable<Garage> GetAllGaragesByCity(string cityName);
        Garage GetGarageById(int id);
        void AddGarage(Garage garage);
        void UpdateGarage(Garage garage);
        void DeleteGarage(int id);
    }
}
