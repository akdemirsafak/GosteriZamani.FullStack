using GosteriZamani.API.Base;
using GosteriZamani.API.Models.Event;

namespace GosteriZamani.API.AbstractServices;

public interface IEventService
{
    Task<AppResult<List<EventResponse>>> GetAllAsync();
    Task<AppResult<EventResponse>> GetByIdAsync(string id);
    Task<AppResult<EventResponse>> CreateAsync(CreateEventDto createEventDto);
    Task<AppResult<EventResponse>> UpdateAsync(UpdateEventDto updateEventDto);
    Task<AppResult<NoContentDto>> DeleteAsync(string id);
}
