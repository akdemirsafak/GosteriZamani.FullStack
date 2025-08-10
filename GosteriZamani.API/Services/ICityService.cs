using GosteriZamani.API.Models.City;

namespace GosteriZamani.API.Services;

public interface ICityService
{
    Task<List<CityResponse>> GetAllAsync();
    Task<CityResponse> GetByIdAsync(string id);
    Task<CityResponse> CreateAsync(CreateCityDto createCityDto);
    Task<CityResponse> UpdateAsync(UpdateCityDto updateCityDto);
    Task DeleteAsync(string id);
}
