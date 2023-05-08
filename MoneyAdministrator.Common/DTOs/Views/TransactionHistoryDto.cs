using MoneyAdministrator.Common.Enums;

namespace MoneyAdministrator.Common.DTOs.Views
{
    public class TransactionHistoryDto
    {
        //hide properties
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Frequency { get; set; }

        //show properties
        public DateTime Date { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public string Installment { get; set; }
        public string CurrencyName { get; set; }
        public decimal Amount { get; set; }
        public bool Concider { get; set; }
        public bool Paid { get; set; }
    }
}