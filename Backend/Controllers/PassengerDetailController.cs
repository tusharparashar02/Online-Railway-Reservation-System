using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PassengerDetailController : ControllerBase
{
    private readonly IPassengerDetailService _service;

    private readonly IReservationService _reservationService;
    public PassengerDetailController(IPassengerDetailService service, IReservationService reservationService)
    {
        _service = service;
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PassengerDetailDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<PassengerDetailDto>> Get(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<PassengerDetailDto>> GetMyDetails()
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized("User identifier missing from token.");

        var detail = await _service.GetByUserIdAsync(userId);
        return Ok(detail); // Returns null if no profile — frontend can handle that
    }

    [HttpPost]
    //[Authorize(Roles = "PASSENGER")]
    public async Task<ActionResult> Create([FromBody] PassengerDetailDto dto)
    {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

        // Attach logged-in user ID
        dto.ApplicationUserId = userId;

        await _service.AddAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = dto.ReservationId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PassengerDetailDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpGet("profile")]
[Authorize]
public async Task<ActionResult<PassengerProfileDto>> GetCompleteProfile()
{
    string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrWhiteSpace(userId))
        return Unauthorized("User ID missing from token.");

    var passengerDetail = await _service.GetByUserIdAsync(userId);
    if (passengerDetail == null)
        return NotFound("Passenger profile not found.");

    var reservations = await _reservationService.GetByUserIdAsync(userId); // Inject IReservationService

    var result = new PassengerProfileDto
    {
        PassengerDetail = passengerDetail,
        Reservations = reservations.ToList()
    };

    return Ok(result);
}
}