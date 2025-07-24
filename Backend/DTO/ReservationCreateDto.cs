public class ReservationCreateDto
{
    public int TrainId { get; set; }
    public DateTime TravelDate { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string TravelClass { get; set; } = string.Empty;
    public List<PassengerDetailDto> Passengers { get; set; } = new();
    public PaymentCreateDto Payment { get; set; } = new();
}