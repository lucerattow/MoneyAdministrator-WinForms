using MoneyAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<CCSummary> CCResumeRepository { get; }
        IRepository<CCSummaryDetail> CCResumeDetailRepository { get; }
        IRepository<CreditCard> CreditCardRepository { get; }
        IRepository<CreditCardType> CreditCardTypeRepository { get; }
        IRepository<Currency> CurrencyRepository { get; }
        IRepository<CurrencyValue> CurrencyValueRepository { get; }
        IRepository<Entity> EntityRepository { get; }
        IRepository<EntityType> EntityTypeRepository { get; }
        IRepository<Salary> SalaryRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        IRepository<TransactionDetail> TransactionDetailRepository { get; }

        void Save();
    }
}
