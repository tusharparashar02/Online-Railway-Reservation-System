public class Train
{
    public int Id { get; set; }
    public string TrainNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SourceStation { get; set; } = string.Empty;
    public string DestinationStation { get; set; } = string.Empty;
    public TimeSpan DepartureTime { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public ICollection<TrainSchedule> Schedules { get; set; } = new List<TrainSchedule>();
    public ICollection<Fare> Fares { get; set; } = new List<Fare>();
}
