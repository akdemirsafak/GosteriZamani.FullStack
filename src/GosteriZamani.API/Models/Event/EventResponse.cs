namespace GosteriZamani.API.Models.Event;

public class EventResponse : ResponseBase
{
    public string Name { get; set; }
    public string? Detail { get; set; }
    public DateTime Date { get; set; }
    public List<EventCategoryDto> Categories { get; set; } = new();
    public string CityName { get; set; }
    public string CityId { get; set; }

}
public class EventCategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}
