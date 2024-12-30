using Car_Management.DTOs;
using Car_Management.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult GetAllCars([FromQuery] string make, [FromQuery] int? fromYear, [FromQuery] int? toYear, [FromQuery] int? garageId)
        {
            var cars = _carService.GetAllCars(make, fromYear, toYear, garageId);
            return Ok(cars);
        }


        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            try
            {
                var car = _carService.GetCarById(id);
                return Ok(car);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddCar([FromBody] CreateCarDto carDto)
        {
            _carService.AddCar(carDto);
            return CreatedAtAction(nameof(GetCarById), new { id = carDto.LicensePlate }, carDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] UpdateCarDto carDto)
        {
            if (id != carDto.Id) return BadRequest("ID mismatch");

            _carService.UpdateCar(carDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            _carService.DeleteCar(id);
            return NoContent();
        }
    }
}
