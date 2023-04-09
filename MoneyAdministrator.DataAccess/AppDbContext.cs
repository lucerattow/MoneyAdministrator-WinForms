using Microsoft.EntityFrameworkCore;
using MoneyAdministrator.DataAccess.Interfaces;
using MoneyAdministrator.Models;

namespace MoneyAdministrator.DataAccess
{
    public class AppDbContext : DbContext
    {
        private string _databasePath;

        public DbSet<CCResumeDetail> CCResumeDetails { get; set; }
        public DbSet<CCResume> CCResumes { get; set; }
        public DbSet<CreditCardType> CreditCardTypes { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyValue> CurrencyValues { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public AppDbContext(string databasePath) 
        {
            this._databasePath = databasePath;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Data Source={_databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Agrego registros predeterminados
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Name = "ARS" },
                new Currency { Id = 2, Name = "USD" }
            );
            modelBuilder.Entity<EntityType>().HasData(
                new Currency { Id = 1, Name = "General" },
                new Currency { Id = 2, Name = "Banco" }
            );
            modelBuilder.Entity<CreditCardType>().HasData(
                new Currency { Id = 1, Name = "Visa" },
                new Currency { Id = 2, Name = "MasterCard" },
                new Currency { Id = 3, Name = "American Express" }
            );
        }
    }
}
