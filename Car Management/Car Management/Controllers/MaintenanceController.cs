using Car_Management.DTOs;
using Car_Management.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [HttpGet]
        public IActionResult GetAllMaintenances([FromQuery] int? carId, [FromQuery] int? garageId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                var maintenances = _maintenanceService.GetAllMaintenances(carId, garageId, startDate, endDate);
                return Ok(maintenances);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMaintenanceById(int id)
        {
            try
            {
                var maintenance = _maintenanceService.GetMaintenanceById(id);
                return Ok(maintenance);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateMaintenance([FromBody] CreateMaintenanceDto maintenanceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _maintenanceService.AddMaintenance(maintenanceDto);
            return CreatedAtAction(nameof(GetMaintenanceById), new { id = maintenanceDto.CarId }, maintenanceDto);
        }



        [HttpPut("{id}")]
        public IActionResult UpdateMaintenance(int id, [FromBody] UpdateMaintenanceDto maintenanceDto)
        {
            if (id != maintenanceDto.Id) return BadRequest("ID mismatch");

            _maintenanceService.UpdateMaintenance(maintenanceDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMaintenance(int id)
        {
            _maintenanceService.DeleteMaintenance(id);
            return NoContent();
        }
        [HttpGet("monthlyRequestsReport")]
        public IActionResult GetMonthlyRequestsReport([FromQuery] int? garageId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    return BadRequest("startDate must be before or equal to endDate.");
                }

                var report = _maintenanceService.GetMonthlyRequestsReport(garageId, startDate, endDate);
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
