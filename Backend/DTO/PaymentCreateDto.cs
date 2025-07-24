public class PaymentCreateDto
{
    public string CreditCardNumber { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    // 🛑 No AmountPaid field here — it will be computed internally
}