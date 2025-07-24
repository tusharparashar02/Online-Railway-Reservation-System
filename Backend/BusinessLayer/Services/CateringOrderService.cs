using AutoMapper;

public class CateringOrderService : ICateringOrderService
{
    private readonly ICateringOrderRepository _repo;
    private readonly IMapper _mapper;

    public CateringOrderService(ICateringOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CateringOrderDto>> GetAllAsync()
    {
        var orders = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<CateringOrderDto>>(orders);
    }

    public async Task<CateringOrderDto?> GetByIdAsync(int id)
    {
        var order = await _repo.GetByIdAsync(id);
        return _mapper.Map<CateringOrderDto>(order);
    }
    public async Task<IEnumerable<CateringOrderDto>> GetByUserIdAsync(string userId)
    {
        var orders = await _repo.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<CateringOrderDto>>(orders);
    }

    public async Task<CateringOrderDto> CreateAsync(CateringOrderDto dto)
    {
        var order = _mapper.Map<CateringOrder>(dto);
        var created = await _repo.CreateAsync(order);
        return _mapper.Map<CateringOrderDto>(created);
    }

    public async Task UpdateAsync(int id, CateringOrderDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return;

        _mapper.Map(dto, existing);
        await _repo.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}