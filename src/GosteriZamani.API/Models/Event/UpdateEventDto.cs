namespace GosteriZamani.API.Models.Event;

public record UpdateEventDto(
    string Id,
    string Name,
    string Detail,
    string Address,
    string CityId,
    string Organizer,
    List<string> CategoryIds,
    DateTime? Date);
