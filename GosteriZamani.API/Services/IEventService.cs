using GosteriZamani.API.Models.Event;

namespace GosteriZamani.API.Services;

public interface IEventService
{
    Task<List<EventResponse>> GetAllAsync();
    Task<EventResponse?> GetByIdAsync(string id);
    Task<EventResponse> CreateAsync(CreateEventDto createEventDto);
    Task<EventResponse> UpdateAsync(UpdateEventDto updateEventDto);
    Task DeleteAsync(string id);
}
