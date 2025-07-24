public interface IReservationService
{
    Task<IEnumerable<ReservationDto>> GetAllAsync();
    Task<ReservationDto?> GetByIdAsync(int id);
    Task<ReservationDto> CreateAsync(ReservationCreateDto dto);
    Task<ReservationDto?> UpdateAsync(int id, ReservationDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<ReservationDto>> GetByUserIdAsync(string userId);
}