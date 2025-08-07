namespace GosteriZamani.API.Entities;

public class City : BaseEntity, IAuditableEntity
{
    //public City()
    //{
    //    Events = new HashSet<Event>();
    //}
    public string Name { get; set; }
    public string Code { get; set; }
    //public string CountryId { get; set; }
    //public Country Country { get; set; }
    //public ICollection<Event> Events { get; set; }

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}

