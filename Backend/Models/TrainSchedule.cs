public class TrainSchedule
{
    public int Id { get; set; }
    public int TrainId { get; set; }
    public Train Train { get; set; } = null!;
    public DateTime TravelDate { get; set; }
    public int AvailableSeats { get; set; }
}
