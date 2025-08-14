using GosteriZamani.API.Base;
using GosteriZamani.API.Models.City;
using GosteriZamani.API.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace GosteriZamani.API.Controllers;


public class CityController : CustomBaseController
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _cityService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await _cityService.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCityDto createCityDto)
    {

        return CreateActionResult(await _cityService.CreateAsync(createCityDto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateCityDto updateCityDto)
    {
        return CreateActionResult(await _cityService.UpdateAsync(updateCityDto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return CreateActionResult(await _cityService.DeleteAsync(id));
    }
}
