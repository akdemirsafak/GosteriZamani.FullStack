using GosteriZamani.API.Models.Country;
using GosteriZamani.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GosteriZamani.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _countryService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _countryService.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCountryDto createCountryDto)
    {

        return Ok(await _countryService.CreateAsync(createCountryDto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateCountryDto updateCountryDto)
    {
        return Ok(await _countryService.UpdateAsync(updateCountryDto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _countryService.DeleteAsync(id);
        return NoContent();
    }
}
