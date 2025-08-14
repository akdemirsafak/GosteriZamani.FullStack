namespace GosteriZamani.API.Models.Event;

public record CreateEventDto(
    string Name,
    string Detail,
    string Address,
    string CityId,
    string Organizer,
    List<string> CategoryIds,
    DateTime? Date);