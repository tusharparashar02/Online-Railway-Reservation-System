public interface ICateringOrderService
{
    Task<IEnumerable<CateringOrderDto>> GetAllAsync();
    Task<CateringOrderDto?> GetByIdAsync(int id);
    Task<IEnumerable<CateringOrderDto>> GetByUserIdAsync(string userId);
    Task<CateringOrderDto> CreateAsync(CateringOrderDto dto);
    Task UpdateAsync(int id, CateringOrderDto dto);
    Task DeleteAsync(int id);
}