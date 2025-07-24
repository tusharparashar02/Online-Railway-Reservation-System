public class WellnessKitRequestDto
{
    public int Id { get; set; }
    public string KitType { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }
    public string StationName { get; set; } = string.Empty;
    public string ApplicationUserId { get; set; } = string.Empty;
}