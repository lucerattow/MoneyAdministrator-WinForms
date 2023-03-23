using Microsoft.EntityFrameworkCore;
using MoneyAdministrator.Models;

namespace MoneyAdministrator.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Agrego registros predeterminados
            modelBuilder.Entity<Currency>().HasData(
                new Currency { Id = 1, Name = "ARS" },
                new Currency { Id = 2, Name = "USD" }
            );
        }

        public void CreateDataBase(string filepath)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={filepath}")
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
        }

        public AppDbContext OpenDataBase(string filepath)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={filepath}")
                .Options;

            return new AppDbContext(options);
        }

        public void DeleteDataBase(string filepath)
        {
            throw new NotImplementedException();
        }

    }
}
