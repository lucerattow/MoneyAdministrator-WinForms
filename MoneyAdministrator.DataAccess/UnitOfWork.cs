using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext _dbContext;

        private IRepository<CCSummary>? _ccResumeRepository;
        private IRepository<CCSummaryDetail>? _ccResumeDetailRepository;
        private IRepository<CreditCard>? _creditCardRepository;
        private IRepository<CreditCardBrand>? _creditCardTypeRepository;
        private IRepository<Currency>? _currencyRepository;
        private IRepository<CurrencyValue>? _currencyValueRepository;
        private IRepository<Entity>? _entityRepository;
        private IRepository<EntityType>? _entityTypeRepository;
        private IRepository<Salary>? _salaryRepository;
        private IRepository<TransactionDetail>? _transactionDetailRepository;
        private IRepository<Transaction>? _transactionRepository;

        public UnitOfWork(string databasePath)
        {
            _dbContext = new AppDbContext(databasePath);
        }

        public IRepository<CCSummary> CCResumeRepository => 
            _ccResumeRepository ??= new Repository<CCSummary>(_dbContext);

        public IRepository<CCSummaryDetail> CCResumeDetailRepository => 
            _ccResumeDetailRepository ??= new Repository<CCSummaryDetail>(_dbContext);

        public IRepository<CreditCard> CreditCardRepository => 
            _creditCardRepository ??= new Repository<CreditCard>(_dbContext);

        public IRepository<CreditCardBrand> CreditCardTypeRepository => 
            _creditCardTypeRepository ??= new Repository<CreditCardBrand>(_dbContext);

        public IRepository<Currency> CurrencyRepository => 
            _currencyRepository ??= new Repository<Currency>(_dbContext);

        public IRepository<CurrencyValue> CurrencyValueRepository => 
            _currencyValueRepository ??= new Repository<CurrencyValue>(_dbContext);

        public IRepository<Entity> EntityRepository => 
            _entityRepository ??= new Repository<Entity>(_dbContext);

        public IRepository<EntityType> EntityTypeRepository => 
            _entityTypeRepository ??= new Repository<EntityType>(_dbContext);

        public IRepository<Salary> SalaryRepository => 
            _salaryRepository ??= new Repository<Salary>(_dbContext);

        public IRepository<TransactionDetail> TransactionDetailRepository => 
            _transactionDetailRepository ??= new Repository<TransactionDetail>(_dbContext);

        public IRepository<Transaction> TransactionRepository => 
            _transactionRepository ??= new Repository<Transaction>(_dbContext);

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
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
