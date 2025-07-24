public class CateringOrderDto
{
    public int Id { get; set; } // ✅ This is the missing property!
    public string MealType { get; set; } = string.Empty;
    public string Preference { get; set; } = string.Empty;
    public DateTime DeliveryDate { get; set; }
    public string DeliveryStation { get; set; } = string.Empty;
    public string ApplicationUserId { get; set; } = string.Empty;
}