using GosteriZamani.API.Models.Country;

namespace GosteriZamani.API.Services;

public interface ICountryService
{
    Task<List<CountryResponse>> GetAllAsync();
    Task<CountryResponse?> GetByIdAsync(string id);
    Task<CountryResponse> CreateAsync(CreateCountryDto createCountryDto);
    Task<CountryResponse> UpdateAsync(UpdateCountryDto updateCountryDto);
    Task DeleteAsync(string id);
}
