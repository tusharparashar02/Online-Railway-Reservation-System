using AutoMapper;

public class PassengerDetailService : IPassengerDetailService
{
    private readonly IPassengerDetailRepository _repo;
    private readonly IMapper _mapper;

    public PassengerDetailService(IPassengerDetailRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PassengerDetailDto>> GetAllAsync()
    {
        var passengers = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<PassengerDetailDto>>(passengers);
    }

    public async Task<PassengerDetailDto?> GetByIdAsync(int id)
    {
        var passenger = await _repo.GetByIdAsync(id);
        return _mapper.Map<PassengerDetailDto>(passenger);
    }
    
    public async Task<PassengerDetailDto?> GetByUserIdAsync(string userId)
    {
        var passenger = await _repo.GetByUserIdAsync(userId);
        return _mapper.Map<PassengerDetailDto>(passenger);
    }

    public async Task AddAsync(PassengerDetailDto dto)
    {
        var passenger = _mapper.Map<PassengerDetail>(dto);
        await _repo.AddAsync(passenger);
    }

    public async Task UpdateAsync(int id, PassengerDetailDto dto)
    {
        var passenger = await _repo.GetByIdAsync(id);
        if (passenger != null)
        {
            _mapper.Map(dto, passenger);
            await _repo.UpdateAsync(passenger);
        }
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}