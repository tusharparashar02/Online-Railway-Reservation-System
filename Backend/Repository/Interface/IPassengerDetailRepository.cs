public interface IPassengerDetailRepository
{
    Task<IEnumerable<PassengerDetail>> GetAllAsync();
    Task<PassengerDetail?> GetByIdAsync(int id);
    Task<PassengerDetail?> GetByUserIdAsync(string userId);
    Task AddAsync(PassengerDetail passenger);
    Task UpdateAsync(PassengerDetail passenger);
    Task DeleteAsync(int id);
}