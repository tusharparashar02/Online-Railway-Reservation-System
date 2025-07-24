using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<PassengerDetail> PassengerDetails { get; set; } = new List<PassengerDetail>();
    public ICollection<CateringOrder> CateringOrders { get; set; } = new List<CateringOrder>();
public ICollection<WellnessKitRequest> WellnessKitRequests { get; set; } = new List<WellnessKitRequest>();
}