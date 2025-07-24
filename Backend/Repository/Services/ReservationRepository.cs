using Microsoft.EntityFrameworkCore;

public class ReservationRepository : IReservationRepository
{
    private readonly RailwayDbContext _context;

    public ReservationRepository(RailwayDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .Include(r => r.TrainSchedule).ThenInclude(ts => ts.Train)
            .Include(r => r.Passengers)
            .Include(r => r.Payment)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations
            .Include(r => r.TrainSchedule).ThenInclude(ts => ts.Train)
            .Include(r => r.Passengers)
            .Include(r => r.Payment)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Reservation> CreateAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<Reservation?> UpdateAsync(int id, Reservation updated)
    {
        var existing = await _context.Reservations.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(updated);
        await _context.SaveChangesAsync();
        return updated;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return false;

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> TrainExistsAsync(int trainId)
    {
        return await _context.Trains.AnyAsync(t => t.Id == trainId);
    }
    public async Task<IEnumerable<Reservation>> GetByUserIdAsync(string userId)
    {
        return await _context.Reservations
            .Where(r => r.UserId == userId)
            .Include(r => r.TrainSchedule)
                .ThenInclude(ts => ts.Train)
            .Include(r => r.Payment)
            .Include(r => r.Passengers)
            .ToListAsync();
    }
}