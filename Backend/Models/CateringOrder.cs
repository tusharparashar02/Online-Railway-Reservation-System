public class CateringOrder
{
    public int Id { get; set; }
    public string MealType { get; set; } = string.Empty; // e.g., Veg, Non-Veg
    public string Preference { get; set; } = string.Empty; // e.g., Jain, Combo, etc.
    public DateTime DeliveryDate { get; set; }
    public string DeliveryStation { get; set; } = string.Empty;

    // 🔗 Link to User
    public string ApplicationUserId { get; set; } = string.Empty;
    public ApplicationUser ApplicationUser { get; set; } = null!;

}