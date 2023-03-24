using Microsoft.EntityFrameworkCore;
using MoneyAdministrator.DataAccess.Interfaces;
using MoneyAdministrator.Models;

namespace MoneyAdministrator.DataAccess
{
    public class AppDbContext : DbContext
    {
        private string _databasePath;

        public DbSet<Entity> Entities { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        public AppDbContext(string databasePath) 
        {
            this._databasePath = databasePath;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Agrego registros predeterminados
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Name = "ARS" },
                new Currency { Id = 2, Name = "USD" }
            );
        }

        //public void CreateDataBase(string filepath)
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //        .UseSqlite($"Data Source={filepath}")
        //        .Options;

        //    var context = new AppDbContext(options);
        //    context.Database.EnsureCreated();
        //}

        //public IAppDbContext OpenDataBase(string filepath)
        //{
        //    var options = new DbContextOptionsBuilder<AppDbContext>()
        //        .UseSqlite($"Data Source={filepath}")
        //        .Options;

        //    return new AppDbContext(options);
        //}

        //public void DeleteDataBase(string filepath)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
