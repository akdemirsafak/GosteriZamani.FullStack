using GosteriZamani.API.Base;
using GosteriZamani.API.Models.Event;
using GosteriZamani.API.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace GosteriZamani.API.Controllers;


public class EventController : CustomBaseController
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _eventService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await _eventService.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateEventDto createEventDto)
    {
        // Here you would typically add logic to save the event data
        return CreateActionResult(await _eventService.CreateAsync(createEventDto));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateEventDto updateEventDto)
    {
        // Here you would typically add logic to update the event data
        return CreateActionResult(await _eventService.UpdateAsync(updateEventDto));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
       return CreateActionResult(await _eventService.DeleteAsync(id));

    }
}
