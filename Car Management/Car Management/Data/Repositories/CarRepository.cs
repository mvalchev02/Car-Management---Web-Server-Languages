using Car_Management.Data.Entities;
using Car_Management.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car_Management.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;
        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            var car = GetCarById(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Car> GetAllCars(string make = null, int? fromYear = null, int? toYear = null, int? garageId = null)
        {
            var query = _context.Cars.Include(c => c.Garages).AsQueryable();

            if (!string.IsNullOrEmpty(make))
                query = query.Where(c => c.Make == make);

            if (fromYear.HasValue)
                query = query.Where(c => c.ProductionYear >= fromYear);

            if (toYear.HasValue)
                query = query.Where(c => c.ProductionYear <= toYear);

            if (garageId.HasValue)
                query = query.Where(c => c.Garages.Any(g => g.Id == garageId));

            return query.ToList();
        }


        public Car GetCarById(int id)
        {
            return _context.Cars.Include(c => c.Garages).FirstOrDefault(c => c.Id == id);
        }

        public void UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }
    }
}
