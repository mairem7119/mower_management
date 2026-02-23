namespace MowerManagement.Api.Domain.Entities;

public class Mower
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? SerialNumber { get; set; }
    public DateOnly? PurchaseDate { get; set; }
    public DateOnly? WarrantyEndDate { get; set; }
    public MowerStatus Status { get; set; }
    public Guid? LocationId { get; set; }
    public string? Notes { get; set; }

    public Location? Location { get; set; }
}
