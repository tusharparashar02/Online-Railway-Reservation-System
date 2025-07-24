public class PassengerDetailDto
{
    public string Name { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Address { get; set; } = string.Empty;
    public int ReservationId { get; set; }
    
    // Include this only in internal calls or protected endpoints
    public string ApplicationUserId { get; set; } = string.Empty;
}