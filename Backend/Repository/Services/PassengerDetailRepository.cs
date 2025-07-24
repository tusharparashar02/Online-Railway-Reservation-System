using Microsoft.EntityFrameworkCore;

public class PassengerDetailRepository : IPassengerDetailRepository
{
    private readonly RailwayDbContext _context;

    public PassengerDetailRepository(RailwayDbContext context) => _context = context;

    public async Task<IEnumerable<PassengerDetail>> GetAllAsync()
        => await _context.Set<PassengerDetail>().Include(p => p.Reservation).ToListAsync();

    public async Task<PassengerDetail?> GetByIdAsync(int id)
        => await _context.Set<PassengerDetail>().Include(p => p.Reservation).FirstOrDefaultAsync(p => p.Id == id);

    public async Task<PassengerDetail?> GetByUserIdAsync(string userId)
    {
        return await _context.PassengerDetails
            .FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
    }

    public async Task AddAsync(PassengerDetail passenger)
    {
        await _context.Set<PassengerDetail>().AddAsync(passenger);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PassengerDetail passenger)
    {
        _context.Set<PassengerDetail>().Update(passenger);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var passenger = await GetByIdAsync(id);
        if (passenger != null)
        {
            _context.Set<PassengerDetail>().Remove(passenger);
            await _context.SaveChangesAsync();
        }
    }
}