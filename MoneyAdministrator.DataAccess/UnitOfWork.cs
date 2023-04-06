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

        private IRepository<CCResume>? _ccResumeRepository;
        private IRepository<CCResumeDetail>? _ccResumeDetailRepository;
        private IRepository<CreditCard>? _creditCardRepository;
        private IRepository<CreditCardType>? _creditCardTypeRepository;
        private IRepository<Currency>? _currencyRepository;
        private IRepository<CurrencyValue>? _currencyValueRepository;
        private IRepository<Entity>? _entityRepository;
        private IRepository<EntityType>? _entityTypeRepository;
        private IRepository<Salary>? _salaryRepository;
        private IRepository<Transaction>? _transactionRepository;
        private IRepository<TransactionDetail>? _transactionDetailRepository;

        public UnitOfWork(string databasePath)
        {
            _context = new AppDbContext(databasePath);
        }

        public IRepository<CCResume> CCResumeRepository => _ccResumeRepository ??= new Repository<CCResume>(_context);
        public IRepository<CCResumeDetail> CCResumeDetailRepository => _ccResumeDetailRepository ??= new Repository<CCResumeDetail>(_context);
        public IRepository<CreditCard> CreditCardRepository => _creditCardRepository ??= new Repository<CreditCard>(_context);
        public IRepository<CreditCardType> CreditCardTypeRepository => _creditCardTypeRepository ??= new Repository<CreditCardType>(_context);
        public IRepository<Currency> CurrencyRepository => _currencyRepository ??= new Repository<Currency>(_context);
        public IRepository<CurrencyValue> CurrencyValueRepository => _currencyValueRepository ??= new Repository<CurrencyValue>(_context);
        public IRepository<Entity> EntityRepository => _entityRepository ??= new Repository<Entity>(_context);
        public IRepository<EntityType> EntityTypeRepository => _entityTypeRepository ??= new Repository<EntityType>(_context);
        public IRepository<Salary> SalaryRepository => _salaryRepository ??= new Repository<Salary>(_context);
        public IRepository<Transaction> TransactionRepository => _transactionRepository ??= new Repository<Transaction>(_context);
        public IRepository<TransactionDetail> TransactionDetailRepository => _transactionDetailRepository ??= new Repository<TransactionDetail>(_context);

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
