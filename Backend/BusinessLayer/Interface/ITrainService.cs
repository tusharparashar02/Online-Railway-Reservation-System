public interface ITrainService
{
    Task<IEnumerable<TrainDto>> GetAllAsync();
     Task<IEnumerable<TrainDto>> GetTrainsByRouteAsync(string source, string destination);

    Task<TrainDto?> GetByIdAsync(int id);
    Task<IEnumerable<TrainDto>> GetByRouteAsync(string source, string destination);
    Task<TrainDto> CreateAsync(TrainDto dto);
    Task<TrainDto?> UpdateAsync(int id, TrainDto dto);
    Task<bool> DeleteAsync(int id);
}