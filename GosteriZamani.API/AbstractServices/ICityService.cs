using GosteriZamani.API.Base;
using GosteriZamani.API.Models.City;

namespace GosteriZamani.API.AbstractServices;

public interface ICityService
{
    Task<AppResult<List<CityResponse>>> GetAllAsync();
    Task<AppResult<CityResponse>> GetByIdAsync(string id);
    Task<AppResult<CityResponse>> CreateAsync(CreateCityDto createCityDto);
    Task<AppResult<CityResponse>> UpdateAsync(UpdateCityDto updateCityDto);
    Task<AppResult<NoContentDto>> DeleteAsync(string id);
}
