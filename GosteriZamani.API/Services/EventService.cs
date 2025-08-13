using GosteriZamani.API.AbstractServices;
using GosteriZamani.API.Base;
using GosteriZamani.API.DbContexts;
using GosteriZamani.API.Entities;
using GosteriZamani.API.Models.Event;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GosteriZamani.API.Services;

public sealed class EventService : IEventService
{
    private readonly GosteriZamaniDbContext _dbContext;

    public EventService(GosteriZamaniDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResult<EventResponse>> CreateAsync(CreateEventDto createEventDto)
    {
        var city = await _dbContext.Cities.FindAsync(createEventDto.CityId);
        var categories = await _dbContext.Categories
            .Where(c => createEventDto.CategoryIds.Contains(c.Id))
            .ToListAsync();
        var @event = new Event
        {
            Name = createEventDto.Name,
            Detail = createEventDto.Detail,
            Address = createEventDto.Address,
            Date = createEventDto.Date ?? DateTime.Now,
            Organizer = createEventDto.Organizer,
            City = city,
            Categories = categories
        };
        await _dbContext.Events.AddAsync(@event);
        await _dbContext.SaveChangesAsync();

        return AppResult<EventResponse>.Success(@event.Adapt<EventResponse>());
    }

    public async Task<AppResult<NoContentDto>> DeleteAsync(string id)
    {
        var @event= await _dbContext.Events.FindAsync(id);
        _dbContext.Events.Remove(@event);
        await _dbContext.SaveChangesAsync();
        return AppResult<NoContentDto>.Success(204);
    }

    public async Task<AppResult<List<EventResponse>>> GetAllAsync()
    {
        var events = await _dbContext.Events
            .Include(cat=>cat.Categories)
            .Include(city=> city.City)
            .AsNoTracking().ToListAsync();


        var eventsResponse = events.Adapt<List<EventResponse>>();

        return AppResult<List<EventResponse>>.Success(eventsResponse);
    }

    public async Task<AppResult<EventResponse>> GetByIdAsync(string id)
    {
        var @event = await _dbContext.Events
            .Include(cat => cat.Categories)
            .Include(city => city.City)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        return AppResult<EventResponse>.Success(@event.Adapt<EventResponse>());

    }

    public async Task<AppResult<EventResponse>> UpdateAsync(UpdateEventDto updateEventDto)
    {
        var @event = await _dbContext.Events.FindAsync(updateEventDto.Id);
        var city = await _dbContext.Cities.FindAsync(updateEventDto.CityId);
        var categories = await _dbContext.Categories
            .Where(c => updateEventDto.CategoryIds.Contains(c.Id))
            .ToListAsync();

        if (@event == null || city == null)
        {
            throw new ArgumentException("Event or City not found");
        }

        @event.Name = updateEventDto.Name;
        @event.Detail = updateEventDto.Detail;
        @event.Address = updateEventDto.Address;
        @event.Date = updateEventDto.Date ?? DateTime.Now;
        @event.Organizer = updateEventDto.Organizer;
        @event.City = city;
        @event.Categories = categories;
        _dbContext.Events.Update(@event);
        await _dbContext.SaveChangesAsync();
        return AppResult<EventResponse>.Success(@event.Adapt<EventResponse>());
    }
}
