namespace GosteriZamani.API.Entities;

public sealed class Event : BaseEntity, IAuditableEntity
{
    public Event()
    {
        Categories = new HashSet<Category>();
    }
    public string Name { get; set; }
    public string Detail { get; set; }
    public string? Address { get; set; }
    public City City { get; set; }
    public string CityId { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime EventDate { get; set; }
    public string Organizer { get; set; }
    public ICollection<Category>? Categories { get; set; }

    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}

