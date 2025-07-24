using Microsoft.EntityFrameworkCore;

public class CateringOrderRepository : ICateringOrderRepository
{
    private readonly RailwayDbContext _context;

    public CateringOrderRepository(RailwayDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CateringOrder>> GetAllAsync() =>
        await _context.CateringOrders.ToListAsync();

    public async Task<IEnumerable<CateringOrder>> GetByUserIdAsync(string userId)
    {
        return await _context.CateringOrders
            .Where(o => o.ApplicationUserId == userId)
            .ToListAsync();
    }
    public async Task<CateringOrder?> GetByIdAsync(int id) =>
        await _context.CateringOrders.FindAsync(id);

    public async Task<CateringOrder> CreateAsync(CateringOrder order)
    {
        _context.CateringOrders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task UpdateAsync(CateringOrder order)
    {
        _context.CateringOrders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var order = await _context.CateringOrders.FindAsync(id);
        if (order != null)
        {
            _context.CateringOrders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}