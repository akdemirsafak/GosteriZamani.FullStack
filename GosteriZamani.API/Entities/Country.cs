
namespace GosteriZamani.API.Entities;

public sealed class Country : BaseEntity, IAuditableEntity
{
    //public Country()
    //{
    //    Cities = new HashSet<City>();
    //}
    public string Name { get; set; }
    public string Code { get; set; }
    //public ICollection<City> Cities { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
