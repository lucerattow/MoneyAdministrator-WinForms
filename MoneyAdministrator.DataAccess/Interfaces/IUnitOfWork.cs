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
        IRepository<Entity> EntityRepository { get; }
        IRepository<Currency> CurrencyRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        IRepository<TransactionDetail> TransactionDetailRepository { get; }
        IRepository<CurrencyValue> CurrencyValueRepository { get; }
        IRepository<Salary> SalaryRepository { get; }
        void Save();
    }
}
