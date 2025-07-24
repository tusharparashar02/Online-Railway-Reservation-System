public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<Reservation> CreateAsync(Reservation reservation);
    Task<Reservation?> UpdateAsync(int id, Reservation updated);
    Task<bool> DeleteAsync(int id);
    Task<bool> TrainExistsAsync(int trainId);
    Task<IEnumerable<Reservation>> GetByUserIdAsync(string userId);
}