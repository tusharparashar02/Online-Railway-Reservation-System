public class PassengerProfileDto
{
    public PassengerDetailDto PassengerDetail { get; set; } = null!;
    public List<ReservationDto> Reservations { get; set; } = new();
}