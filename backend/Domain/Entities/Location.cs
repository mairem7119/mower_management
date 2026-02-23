namespace MowerManagement.Api.Domain.Entities;

public class Location
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Address { get; set; }

    public ICollection<Mower> Mowers { get; set; } = new List<Mower>();
}
