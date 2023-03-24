using MoneyAdministrator.DataAccess.Interfaces;
using MoneyAdministrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAdministrator.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IRepository<Entity>? _entityRepository;
        private IRepository<Currency>? _currencyRepository;
        private IRepository<Transaction>? _transactionRepository;
        private IRepository<TransactionDetail>? _transactionDetailRepository;

        public UnitOfWork(string databasePath)
        {
            _context = new AppDbContext(databasePath);
        }

        public IRepository<Entity> EntityRepository => 
            _entityRepository ??= new Repository<Entity>(_context);
        public IRepository<Currency> CurrencyRepository => 
            _currencyRepository ??= new Repository<Currency>(_context);
        public IRepository<Transaction> TransactionRepository => 
            _transactionRepository ??= new Repository<Transaction>(_context);
        public IRepository<TransactionDetail> TransactionDetailRepository => 
            _transactionDetailRepository ??= new Repository<TransactionDetail>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
