using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MowerManagement.Api.Contracts.Locations;
using MowerManagement.Api.Data;
using MowerManagement.Api.Domain.Entities;

namespace MowerManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LocationsController : ControllerBase
{
    private readonly AppDbContext _db;

    public LocationsController(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>Get all locations.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LocationResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LocationResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var list = await _db.Locations
            .OrderBy(l => l.Name)
            .Select(l => new LocationResponse
            {
                Id = l.Id,
                Name = l.Name,
                Address = l.Address
            })
            .ToListAsync(cancellationToken);
        return Ok(list);
    }

    /// <summary>Get a location by id.</summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LocationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LocationResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var location = await _db.Locations
            .AsNoTracking()
            .Where(l => l.Id == id)
            .Select(l => new LocationResponse
            {
                Id = l.Id,
                Name = l.Name,
                Address = l.Address
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (location == null)
            return NotFound();

        return Ok(location);
    }

    /// <summary>Create a new location.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(LocationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LocationResponse>> Create(
        [FromBody] LocationCreateRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = new Location
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Address = string.IsNullOrWhiteSpace(request.Address) ? null : request.Address.Trim()
        };

        _db.Locations.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        var response = new LocationResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address
        };

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, response);
    }

    /// <summary>Update an existing location.</summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(LocationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LocationResponse>> Update(
        Guid id,
        [FromBody] LocationUpdateRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = await _db.Locations.FindAsync([id], cancellationToken);
        if (entity == null)
            return NotFound();

        entity.Name = request.Name.Trim();
        entity.Address = string.IsNullOrWhiteSpace(request.Address) ? null : request.Address.Trim();

        await _db.SaveChangesAsync(cancellationToken);

        return Ok(new LocationResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address
        });
    }

    /// <summary>Delete a location.</summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _db.Locations.FindAsync([id], cancellationToken);
        if (entity == null)
            return NotFound();

        _db.Locations.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
