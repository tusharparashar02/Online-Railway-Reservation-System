using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CateringOrderController : ControllerBase
{
    private readonly ICateringOrderService _service;

    public CateringOrderController(ICateringOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _service.GetByIdAsync(id);
        return order is null ? NotFound() : Ok(order);
    }
    [HttpGet("my-orders")]
    [Authorize] // Optional: ensures only logged-in users can access
    public async Task<IActionResult> GetMyCateringOrders()
    {
        // Get the logged-in user's ID
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User ID not found in token.");

        // Call the service to get only this user's catering orders
        var orders = await _service.GetByUserIdAsync(userId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CateringOrderDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CateringOrderDto dto)
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
}