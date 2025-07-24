public interface IWellnessKitRequestService
{
    Task<IEnumerable<WellnessKitRequestDto>> GetAllAsync();
    Task<WellnessKitRequestDto?> GetByIdAsync(int id);
    Task<IEnumerable<WellnessKitRequestDto>> GetByUserIdAsync(string userId);
    Task<WellnessKitRequestDto> CreateAsync(WellnessKitRequestDto dto);
    Task UpdateAsync(int id, WellnessKitRequestDto dto);
    Task DeleteAsync(int id);
}