using AutoMapper;

public class WellnessKitRequestService : IWellnessKitRequestService
{
    private readonly IWellnessKitRequestRepository _repo;
    private readonly IMapper _mapper;

    public WellnessKitRequestService(IWellnessKitRequestRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WellnessKitRequestDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<WellnessKitRequestDto>>(items);
    }

    public async Task<WellnessKitRequestDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return _mapper.Map<WellnessKitRequestDto>(entity);
    }
    public async Task<IEnumerable<WellnessKitRequestDto>> GetByUserIdAsync(string userId)
    {
        var requests = await _repo.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<WellnessKitRequestDto>>(requests);
    }
    public async Task<WellnessKitRequestDto> CreateAsync(WellnessKitRequestDto dto)
    {
        var entity = _mapper.Map<WellnessKitRequest>(dto);
        var created = await _repo.CreateAsync(entity);
        return _mapper.Map<WellnessKitRequestDto>(created);
    }

    public async Task UpdateAsync(int id, WellnessKitRequestDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing is null) return;

        _mapper.Map(dto, existing);
        await _repo.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id) =>
        await _repo.DeleteAsync(id);
}