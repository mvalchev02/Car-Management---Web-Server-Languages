using Car_Management.Data.Entities;
using Car_Management.DTOs;
using Car_Management.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GarageController : ControllerBase
{
    private readonly IGarageService _garageService;

    public GarageController(IGarageService garageService)
    {
        _garageService = garageService;
    }

    [HttpGet]
    public IActionResult GetAllGaragesByCity([FromQuery] string city)
    {
        var garages = _garageService.GetAllGaragesByCity(city);
        return Ok(garages);
    }

    [HttpGet("{id}")]
    public IActionResult GetGarageById(int id)
    {
        try
        {
            var garage = _garageService.GetGarageById(id);
            return Ok(garage);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateGarage([FromBody] GarageDto garage )
    {
        _garageService.AddGarage(garage);
        return CreatedAtAction(nameof(GetGarageById), new { id = garage .Id }, garage);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGarage(int id, [FromBody] GarageDto garage)
    {
        if (id != garage.Id) return BadRequest("ID mismatch");

        _garageService.UpdateGarage(garage);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGarage(int id)
    {
        _garageService.DeleteGarage(id);
        return NoContent();
    }
}
