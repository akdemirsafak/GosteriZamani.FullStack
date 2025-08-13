using GosteriZamani.API.Base;
using GosteriZamani.API.Models.Country;
using GosteriZamani.API.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace GosteriZamani.API.Controllers;


public class CountryController : CustomBaseController
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _countryService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await _countryService.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCountryDto createCountryDto)
    {

        return CreateActionResult(await _countryService.CreateAsync(createCountryDto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateCountryDto updateCountryDto)
    {
        return CreateActionResult(await _countryService.UpdateAsync(updateCountryDto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
       return CreateActionResult(await _countryService.DeleteAsync(id));
  
    }
}
