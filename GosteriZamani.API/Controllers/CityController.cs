using GosteriZamani.API.Models.City;
using GosteriZamani.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GosteriZamani.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _cityService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _cityService.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCityDto createCityDto)
    {

        return Ok(await _cityService.CreateAsync(createCityDto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateCityDto updateCityDto)
    {
        return Ok(await _cityService.UpdateAsync(updateCityDto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _cityService.DeleteAsync(id);
        return NoContent();
    }
}
