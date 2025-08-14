using GosteriZamani.API.AbstractServices;
using GosteriZamani.API.Base;
using GosteriZamani.API.DbContexts;
using GosteriZamani.API.Entities;
using GosteriZamani.API.Models.Country;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GosteriZamani.API.Services;

public class CountryService : ICountryService
{
    private readonly GosteriZamaniDbContext _context;

    public CountryService(GosteriZamaniDbContext context)
    {
        _context = context;
    }

    public async Task<AppResult<CountryResponse>> CreateAsync(CreateCountryDto createCountryDto)
    {
        var existingCountry = await _context.Countries
            .FirstOrDefaultAsync(c => c.Name == createCountryDto.Name);
        if (existingCountry is not null)
        {
            throw new ArgumentException("Country already exists.");
        }
        var country = createCountryDto.Adapt<Country>();
        await _context.Countries.AddAsync(country);
        await _context.SaveChangesAsync();
        return AppResult<CountryResponse>.Success(country.Adapt<CountryResponse>(),201);
    }

    public async Task<AppResult<NoContentDto>> DeleteAsync(string id)
    {
        var country = await _context.Countries.FindAsync(id);
        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();
        return AppResult<NoContentDto>.Success(204);
    }

    public async Task<AppResult<List<CountryResponse>>> GetAllAsync()
    {
        var countries = await _context.Countries.AsNoTracking().ToListAsync();

        return AppResult<List<CountryResponse>>.Success(countries.Adapt<List<CountryResponse>>());
    }

    public async Task<AppResult<CountryResponse>> GetByIdAsync(string id)
    {
        var country = await _context.Countries.FindAsync(id);

        return AppResult<CountryResponse>.Success(country.Adapt<CountryResponse>());
    }

    public async Task<AppResult<CountryResponse>> UpdateAsync(UpdateCountryDto updateCountryDto)
    {
        var country = await _context.Countries.FindAsync(updateCountryDto.Id);
        country.Name = updateCountryDto.Name;
        country.Code = updateCountryDto.Code;
        await _context.SaveChangesAsync();
        return AppResult<CountryResponse>.Success(country.Adapt<CountryResponse>(),200);

    }
}
