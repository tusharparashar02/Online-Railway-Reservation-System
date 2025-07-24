public interface ICateringOrderRepository
{
    Task<IEnumerable<CateringOrder>> GetAllAsync();
    Task<CateringOrder?> GetByIdAsync(int id);
    Task<IEnumerable<CateringOrder>> GetByUserIdAsync(string userId);
    Task<CateringOrder> CreateAsync(CateringOrder order);
    Task UpdateAsync(CateringOrder order);
    Task DeleteAsync(int id);
}