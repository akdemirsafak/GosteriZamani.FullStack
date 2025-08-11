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

    public async Task<EventResponse> CreateAsync(CreateEventDto createEventDto)
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
        return @event.Adapt<EventResponse>();
    }

    public async Task DeleteAsync(string id)
    {
        var @event= await _dbContext.Events.FindAsync(id);
        _dbContext.Events.Remove(@event);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<EventResponse>> GetAllAsync()
    {
        var events = await _dbContext.Events
            .Include(cat=>cat.Categories)
            .Include(city=> city.City)
            .AsNoTracking().ToListAsync();
        return events.Select(e => new EventResponse
        {
            Id = e.Id,
            Name = e.Name,
            Detail = e.Detail,
            Date = e.Date,
            Categories = e.Categories.Adapt<List<EventCategoryDto>>(),
            City = e.City.Name
        }).ToList();
    }

    public async Task<EventResponse?> GetByIdAsync(string id)
    {
        var @event = await _dbContext.Events
            .Include(cat => cat.Categories)
            .Include(city => city.City)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        return @event.Adapt<EventResponse>();
    }

    public async Task<EventResponse> UpdateAsync(UpdateEventDto updateEventDto)
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
        return @event.Adapt<EventResponse>();
    }
}
