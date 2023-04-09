namespace MoneyAdministrator.DTOs
{
    public class TransactionDto
    {
        //properties
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public string Installment { get; set; }
        public string CurrencyName { get; set; }
        public decimal Amount { get; set; }
    }
}