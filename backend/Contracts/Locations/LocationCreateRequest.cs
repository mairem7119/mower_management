using System.ComponentModel.DataAnnotations;

namespace MowerManagement.Api.Contracts.Locations;

public class LocationCreateRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    public string? Address { get; set; }
}
