public interface IWellnessKitRequestRepository
{
    Task<IEnumerable<WellnessKitRequest>> GetAllAsync();
    Task<WellnessKitRequest?> GetByIdAsync(int id);
    Task<IEnumerable<WellnessKitRequest>> GetByUserIdAsync(string userId);
    Task<WellnessKitRequest> CreateAsync(WellnessKitRequest request);
    Task UpdateAsync(WellnessKitRequest request);
    Task DeleteAsync(int id);
}