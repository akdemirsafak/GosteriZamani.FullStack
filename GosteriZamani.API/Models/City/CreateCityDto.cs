namespace GosteriZamani.API.Models.City;

public record CreateCityDto(string Name, string CountryId, string? Code=null);
