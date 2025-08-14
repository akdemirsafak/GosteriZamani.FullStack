using GosteriZamani.API.AbstractServices;
using GosteriZamani.API.Base;
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

    public async Task<AppResult<CityResponse>> CreateAsync(CreateCityDto createCityDto)
    {
        City existingCity = await _context.Cities
            .FirstOrDefaultAsync(c => c.Name == createCityDto.Name && c.CountryId == createCityDto.CountryId);
        if (existingCity is not null)
        {
            AppResult<CityResponse> result = AppResult<CityResponse>.Fail("City already exists in this country.");
        }
        City city= createCityDto.Adapt<City>();
        var country = await _context.Countries.FindAsync(createCityDto.CountryId);
        city.Country = country;
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();
        return AppResult<CityResponse>.Success(city.Adapt<CityResponse>(), 201);
    }

    public async Task<AppResult<NoContentDto>> DeleteAsync(string id)
    {
        City city= await _context.Cities.FindAsync(id);
        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
        return AppResult<NoContentDto>.Success(204);
    }

    public async Task<AppResult<List<CityResponse>>> GetAllAsync()
    {
        var cities= await _context.Cities.Include(x=>x.Country).AsNoTracking().ToListAsync();
        var cityResponse= cities.Adapt<List<CityResponse>>();
        return AppResult<List<CityResponse>>.Success(cityResponse);
    }

    public async Task<AppResult<CityResponse>> GetByIdAsync(string id)
    {
        City? city = await _context.Cities.Include(x=>x.Country).FirstOrDefaultAsync(x=>x.Id==id);

        return AppResult<CityResponse>.Success(city.Adapt<CityResponse>());
    }

    public async Task<AppResult<CityResponse>> UpdateAsync(UpdateCityDto updateCityDto)
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
        return AppResult<CityResponse>.Success(city.Adapt<CityResponse>(), 200);

    }
}
