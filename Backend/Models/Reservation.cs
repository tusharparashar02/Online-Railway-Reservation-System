public class Reservation
{
    public int Id { get; set; }
    public string PNR { get; set; } = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public int TrainScheduleId { get; set; }
    public TrainSchedule TrainSchedule { get; set; } = null!;
    public string TravelClass { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; } = DateTime.UtcNow;
    public ICollection<PassengerDetail> Passengers { get; set; } = new List<PassengerDetail>();
    public PaymentDetail Payment { get; set; } = null!;
}
