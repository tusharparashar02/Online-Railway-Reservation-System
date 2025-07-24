public interface ITrainRepository
{
    Task<IEnumerable<Train>> GetAllAsync();
    Task<IEnumerable<Train>> GetTrainsByRouteAsync(string source, string destination);

    Task<Train?> GetByIdAsync(int id);
    Task<IEnumerable<Train>> GetByRouteAsync(string source, string destination);
    Task<Train> CreateAsync(Train train);
    Task<Train?> UpdateAsync(int id, Train train);
    Task<bool> DeleteAsync(int id);
}