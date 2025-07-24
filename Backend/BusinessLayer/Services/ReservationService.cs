using AutoMapper;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _repo;
    private readonly ITrainRepository _trainRepo;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository repo, ITrainRepository trainRepo, IMapper mapper)
    {
        _repo = repo;
        _trainRepo = trainRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllAsync()
    {
        var reservations = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }

    public async Task<ReservationDto?> GetByIdAsync(int id)
    {
        var reservation = await _repo.GetByIdAsync(id);
        return reservation == null ? null : _mapper.Map<ReservationDto>(reservation);
    }

    public async Task<ReservationDto> CreateAsync(ReservationCreateDto dto)
    {
        // 🔍 Normalize travel class string from input
        var normalizedClass = dto.TravelClass?.Trim().ToLowerInvariant();

        // 🔍 Validate Train exists
        var train = await _trainRepo.GetByIdAsync(dto.TrainId);
        if (train == null)
            throw new InvalidOperationException($"Train with ID {dto.TrainId} does not exist.");

        // 🎯 Normalize fare classes before matching
        var fare = train.Fares.FirstOrDefault(f =>
            f.Class?.Trim().ToLowerInvariant() == normalizedClass);

        if (fare == null)
            throw new InvalidOperationException($"Fare class '{dto.TravelClass}' not found for Train ID {dto.TrainId}.");

        // 💰 Calculate total fare
        decimal totalFare = dto.Passengers.Sum(p => p.Age < 12 ? fare.ChildFare : fare.AdultFare);

        // 🏗️ Map DTO to Entity
        var reservation = _mapper.Map<Reservation>(dto);

        // 🗓️ Assign train schedule
        reservation.TrainSchedule = new TrainSchedule
        {
            TrainId = dto.TrainId,
            TravelDate = dto.TravelDate,
            AvailableSeats = 0
        };

        // 💳 Assign auto-calculated fare
        reservation.Payment.AmountPaid = totalFare;

        // 📝 Save reservation
        var saved = await _repo.CreateAsync(reservation);
        return _mapper.Map<ReservationDto>(saved);
    }

    public async Task<ReservationDto?> UpdateAsync(int id, ReservationDto dto)
    {
        var entity = _mapper.Map<Reservation>(dto);
        var updated = await _repo.UpdateAsync(id, entity);
        return updated == null ? null : _mapper.Map<ReservationDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<ReservationDto>> GetByUserIdAsync(string userId)
    {
        var reservations = await _repo.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
    }
}