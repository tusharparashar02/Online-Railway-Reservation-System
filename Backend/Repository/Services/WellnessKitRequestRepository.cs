using Microsoft.EntityFrameworkCore;

public class WellnessKitRequestRepository : IWellnessKitRequestRepository
{
    private readonly RailwayDbContext _context;

    public WellnessKitRequestRepository(RailwayDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WellnessKitRequest>> GetAllAsync() =>
        await _context.WellnessKitRequests.ToListAsync();

    public async Task<WellnessKitRequest?> GetByIdAsync(int id) =>
        await _context.WellnessKitRequests.FindAsync(id);

    public async Task<IEnumerable<WellnessKitRequest>> GetByUserIdAsync(string userId)
    {
        return await _context.WellnessKitRequests
            .Where(r => r.ApplicationUserId == userId)
            .ToListAsync();
    }

    public async Task<WellnessKitRequest> CreateAsync(WellnessKitRequest request)
    {
        _context.WellnessKitRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task UpdateAsync(WellnessKitRequest request)
    {
        _context.WellnessKitRequests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.WellnessKitRequests.FindAsync(id);
        if (entity != null)
        {
            _context.WellnessKitRequests.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}