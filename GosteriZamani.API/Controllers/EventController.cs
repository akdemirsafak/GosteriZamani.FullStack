using GosteriZamani.API.Models.Event;
using GosteriZamani.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GosteriZamani.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _eventService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _eventService.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateEventDto createEventDto)
    {
        // Here you would typically add logic to save the event data
        return Ok(await _eventService.CreateAsync(createEventDto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateEventDto updateEventDto)
    {
        // Here you would typically add logic to update the event data
        return Ok(await _eventService.UpdateAsync(updateEventDto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _eventService.DeleteAsync(id);
        return NoContent();
    }
}
