using GosteriZamani.API.DbContexts;
using GosteriZamani.API.Entities;
using GosteriZamani.API.Models.City;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GosteriZamani.API.Services;

public class CityService : ICityService
{
    private readonly GosteriZamaniDbContext _context;

    public CityService(GosteriZamaniDbContext context)
    {
        _context = context;
    }

    public async Task<CityResponse> CreateAsync(CreateCityDto createCityDto)
    {
        City existingCity = await _context.Cities
            .FirstOrDefaultAsync(c => c.Name == createCityDto.Name && c.CountryId == createCityDto.CountryId);
        if (existingCity is not null)
        {
            throw new ArgumentException("City already exists in the specified country.");
        }
        City city= createCityDto.Adapt<City>();
        var country = await _context.Countries.FindAsync(createCityDto.CountryId);
        city.Country = country;
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();
        return city.Adapt<CityResponse>();
    }

    public async Task DeleteAsync(string id)
    {
        City city= await _context.Cities.FindAsync(id);
        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CityResponse>> GetAllAsync()
    {
        var cities= await _context.Cities.Include(x=>x.Country).AsNoTracking().ToListAsync();
        var cityResponse= cities.Adapt<List<CityResponse>>();
        return cityResponse;
    }

    public async Task<CityResponse> GetByIdAsync(string id)
    {
        City? city = await _context.Cities.Include(x=>x.Country).FirstOrDefaultAsync(x=>x.Id==id);
        return city.Adapt<CityResponse>();
    }

    public async Task<CityResponse> UpdateAsync(UpdateCityDto updateCityDto)
    {
        var country = await _context.Countries.FindAsync(updateCityDto.CountryId);
        if (country == null)
        {
            throw new ArgumentException("Country not found");
        }
        City city = await _context.Cities.FindAsync(updateCityDto.Id);

        city.Name = updateCityDto.Name;
        city.Code = updateCityDto.Code;

        city.Country = country;
        city.CountryId = updateCityDto.CountryId;
        _context.Cities.Update(city);
        await _context.SaveChangesAsync();
        return city.Adapt<CityResponse>();
    }
}
