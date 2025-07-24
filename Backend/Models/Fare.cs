public class Fare
{
    public int Id { get; set; }
    public int TrainId { get; set; }
    public Train Train { get; set; } = null!;
    public string Class { get; set; } = string.Empty; // e.g., Economy, Business
    public decimal AdultFare { get; set; }
    public decimal ChildFare { get; set; }
}
