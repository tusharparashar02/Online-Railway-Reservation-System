public class PaymentDetailDto
{
    public int Id { get; set; }
    public string CreditCardNumber { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public decimal AmountPaid { get; set; }
    public DateTime PaymentDate { get; set; }
}
