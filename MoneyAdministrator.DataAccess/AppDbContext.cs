using Microsoft.EntityFrameworkCore;
using MoneyAdministrator.DataAccess.Interfaces;
using MoneyAdministrator.Models;

namespace MoneyAdministrator.DataAccess
{
    public class AppDbContext : DbContext
    {
        private string _databasePath;

        public DbSet<CCSummaryDetail> CCSummaryDetails { get; set; }
        public DbSet<CCSummary> CCSummaries { get; set; }
        public DbSet<CreditCardBank> CreditCardBanks { get; set; }
        public DbSet<CreditCardBrand> CreditCardBrands { get; set; }
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
                new EntityType { Id = 1, Name = "General" }
            );
            modelBuilder.Entity<CreditCardBank>().HasData(
                new CreditCardBank { Id = 1, ImportSupport = true, Name = "HSBC" },
                new CreditCardBank { Id = 2, ImportSupport = true, Name = "Supervielle" }
            );
            modelBuilder.Entity<CreditCardBrand>().HasData(
                new CreditCardBrand { Id = 1, Name = "Visa" },
                new CreditCardBrand { Id = 2, Name = "MasterCard" }
            );
        }
    }
}
