using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("mine")]
    [Authorize]
    public async Task<IActionResult> GetMyReservations()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized("User identifier missing from token.");

        var reservations = await _service.GetByUserIdAsync(userId);
        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var data = await _service.GetByIdAsync(id);
        return data == null ? NotFound() : Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ReservationCreateDto dto)
    {
        try
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReservationDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}