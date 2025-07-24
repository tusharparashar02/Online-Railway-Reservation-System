using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TrainsController : ControllerBase
{
    private readonly ITrainService _service;

    public TrainsController(ITrainService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());
    
    [HttpGet("search")]
    public async Task<IActionResult> GetTrainsByRoute([FromQuery] string source, [FromQuery] string destination)
    {
        var trains = await _service.GetTrainsByRouteAsync(source, destination);
        return Ok(trains);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var train = await _service.GetByIdAsync(id);
        return train == null ? NotFound() : Ok(train);
    }

    // [HttpGet("search")]
    // public async Task<IActionResult> Search([FromQuery] string source, [FromQuery] string destination)
    // {
    //     var trains = await _service.GetByRouteAsync(source, destination);
    //     return Ok(trains);
    // }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TrainDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TrainDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}