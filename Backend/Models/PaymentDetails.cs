
public class PaymentDetail
{
    public int Id { get; set; }
    public string CreditCardNumber { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;
}
