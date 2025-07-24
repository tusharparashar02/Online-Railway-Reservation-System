using Microsoft.EntityFrameworkCore;

public class TrainRepository : ITrainRepository
{
    private readonly RailwayDbContext _context;

    public TrainRepository(RailwayDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Train>> GetAllAsync() =>
        await _context.Trains.Include(t => t.Fares).ToListAsync();

    public async Task<IEnumerable<Train>> GetTrainsByRouteAsync(string source, string destination)
    {
        return await _context.Trains
            .Where(t => t.SourceStation == source && t.DestinationStation == destination)
            .Include(t => t.Fares)
            .ToListAsync();
    }

    public async Task<Train?> GetByIdAsync(int id) =>
        await _context.Trains.Include(t => t.Fares).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IEnumerable<Train>> GetByRouteAsync(string source, string destination) =>
        await _context.Trains
            .Where(t => t.SourceStation == source && t.DestinationStation == destination)
            .Include(t => t.Fares)
            .ToListAsync();

    public async Task<Train> CreateAsync(Train train)
    {
        // ✅ Auto-assign default fare classes to every new train
        train.Fares = new List<Fare>
        {
            new Fare { Class = "Sleeper", AdultFare = 300, ChildFare = 200 },
            new Fare { Class = "AC", AdultFare = 600, ChildFare = 400 },
            new Fare { Class = "First Class", AdultFare = 900, ChildFare = 600 }
        };

        await _context.Trains.AddAsync(train);
        await _context.SaveChangesAsync();
        return train;
    }

    public async Task<Train?> UpdateAsync(int id, Train train)
    {
        var existing = await _context.Trains
            .Include(t => t.Fares)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (existing == null) return null;

        // ✅ Preserve or assign default fares if missing
        train.Fares = existing.Fares.Any() ? existing.Fares : new List<Fare>
        {
            new Fare { Class = "Sleeper", AdultFare = 300, ChildFare = 200 },
            new Fare { Class = "AC", AdultFare = 600, ChildFare = 400 },
            new Fare { Class = "First Class", AdultFare = 900, ChildFare = 600 }
        };

        _context.Entry(existing).CurrentValues.SetValues(train);
        await _context.SaveChangesAsync();
        return train;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var train = await _context.Trains.FindAsync(id);
        if (train == null) return false;

        _context.Trains.Remove(train);
        await _context.SaveChangesAsync();
        return true;
    }
}