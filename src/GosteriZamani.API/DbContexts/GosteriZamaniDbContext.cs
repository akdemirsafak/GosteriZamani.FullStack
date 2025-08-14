using GosteriZamani.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GosteriZamani.API.DbContexts;

public class GosteriZamaniDbContext : DbContext
{
    public GosteriZamaniDbContext(DbContextOptions<GosteriZamaniDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Event> Events { get; set; }
    
}
