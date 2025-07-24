using AutoMapper;

public class TrainService : ITrainService
{
    private readonly ITrainRepository _repo;
    private readonly IMapper _mapper;

    public TrainService(ITrainRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TrainDto>> GetAllAsync()
    {
        var trains = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<TrainDto>>(trains);
    }

    public async Task<TrainDto?> GetByIdAsync(int id)
    {
        var train = await _repo.GetByIdAsync(id);
        return train == null ? null : _mapper.Map<TrainDto>(train);
    }

    public async Task<IEnumerable<TrainDto>> GetByRouteAsync(string source, string destination)
    {
        var trains = await _repo.GetByRouteAsync(source, destination);
        return _mapper.Map<IEnumerable<TrainDto>>(trains);
    }
    public async Task<IEnumerable<TrainDto>> GetTrainsByRouteAsync(string source, string destination)
    {
        var trains = await _repo.GetTrainsByRouteAsync(source, destination);
        return _mapper.Map<IEnumerable<TrainDto>>(trains);
    }

    public async Task<TrainDto> CreateAsync(TrainDto dto)
    {
        var entity = _mapper.Map<Train>(dto);
        var saved = await _repo.CreateAsync(entity);
        return _mapper.Map<TrainDto>(saved);
    }

    public async Task<TrainDto?> UpdateAsync(int id, TrainDto dto)
    {
        var updated = await _repo.UpdateAsync(id, _mapper.Map<Train>(dto));
        return updated == null ? null : _mapper.Map<TrainDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id) =>
        await _repo.DeleteAsync(id);
}