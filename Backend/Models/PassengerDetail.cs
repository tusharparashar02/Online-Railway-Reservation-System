public class PassengerDetail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Address { get; set; } = string.Empty;

    public int? ReservationId { get; set; } // 👈 Nullable foreign key
    public Reservation? Reservation { get; set; }


    // 🔗 Link to Identity User
    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}