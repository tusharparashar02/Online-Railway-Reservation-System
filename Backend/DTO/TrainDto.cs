public class TrainDto
{
    public int Id { get; set; }
    public string TrainNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SourceStation { get; set; } = string.Empty;
    public string DestinationStation { get; set; } = string.Empty;
    public TimeSpan DepartureTime { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public List<FareDto> Fares { get; set; } = new();
}