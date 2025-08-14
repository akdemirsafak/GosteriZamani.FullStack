using GosteriZamani.API.Base;
using GosteriZamani.API.Models.Country;

namespace GosteriZamani.API.AbstractServices;

public interface ICountryService
{
    Task<AppResult<List<CountryResponse>>> GetAllAsync();
    Task<AppResult<CountryResponse>> GetByIdAsync(string id);
    Task<AppResult<CountryResponse>> CreateAsync(CreateCountryDto createCountryDto);
    Task<AppResult<CountryResponse>> UpdateAsync(UpdateCountryDto updateCountryDto);
    Task<AppResult<NoContentDto>> DeleteAsync(string id);
}
