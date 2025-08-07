
namespace GosteriZamani.API.Entities;

public class Ticket : BaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    //public string EventId { get; set; }
    //public Event Event { get; set; }
    public DateTime CreatedAt { get ; set ; }
    public string CreatedBy { get ; set ; }
    public DateTime? UpdatedAt { get ; set ; }
    public string? UpdatedBy { get ; set ; }
    public DateTime? DeletedAt { get ; set ; }
    public string? DeletedBy { get ; set ; }
}
