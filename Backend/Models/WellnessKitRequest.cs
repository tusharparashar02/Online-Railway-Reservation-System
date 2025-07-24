public class WellnessKitRequest
{
    public int Id { get; set; }
    public string KitType { get; set; } = string.Empty; // e.g., Basic, Deluxe, Medical Aid
    public string Notes { get; set; } = string.Empty;   // Optional instructions

    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public string StationName { get; set; } = string.Empty;

    // 🔗 Link to User
    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = null!;

}