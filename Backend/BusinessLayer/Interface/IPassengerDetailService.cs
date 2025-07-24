public interface IPassengerDetailService
{
    Task<IEnumerable<PassengerDetailDto>> GetAllAsync();
    Task<PassengerDetailDto?> GetByIdAsync(int id);
    Task<PassengerDetailDto?> GetByUserIdAsync(string userId);
    Task AddAsync(PassengerDetailDto dto);
    Task UpdateAsync(int id, PassengerDetailDto dto);
    Task DeleteAsync(int id);
}