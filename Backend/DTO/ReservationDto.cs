
public class ReservationDto
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string TravelClass { get; set; } = string.Empty;
    public string PNR { get; set; } = string.Empty;
    public TrainDto Train { get; set; } = null!;
    public DateTime TravelDate { get; set; }
    public List<PassengerDetailDto> Passengers { get; set; } = new();
    public PaymentDetailDto Payment { get; set; } = null!;
}