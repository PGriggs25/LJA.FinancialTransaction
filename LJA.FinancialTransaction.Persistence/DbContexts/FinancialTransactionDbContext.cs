using LJA.FinancialTransaction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace LJA.FinancialTransaction.Persistence.DbContexts
{
    public class FinancialTransactionDbContext : DbContext
    {
        public FinancialTransactionDbContext(DbContextOptions<FinancialTransactionDbContext> options) : base(options)
        {
        }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<Sources> Sources { get; set; }

        public DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Require that the Name column is non‑nullable
            modelBuilder.Entity<Categories>().Property(x => x.Name).IsRequired(true);

            // Require that the Name column is non‑nullable
            modelBuilder.Entity<Sources>().Property(x => x.Name).IsRequired(true);

            // Require that the Description column is non‑nullable
            modelBuilder.Entity<Transactions>().Property(x => x.Description).IsRequired(true);
            
            // Configure all decimal properties with precision 18 and scale 2
            var decimalProps = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => (System.Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

                        foreach (var property in decimalProps)
                        {
                            property.SetPrecision(18);
                            property.SetScale(2);
                        }

            // Link Transactions to Sources via SourceId
            modelBuilder.Entity<Transactions>()
                .HasOne<Sources>()
                .WithMany()
                .HasForeignKey(t => t.SourceId)
                .OnDelete(DeleteBehavior.NoAction);

            // Link Transactions to Categories via CategoryId
            modelBuilder.Entity<Transactions>()
                .HasOne<Categories>()
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed lookup and sample transaction data
            modelBuilder.Entity<Categories>().HasData(
                new Categories { Id = 1, Name = "Revenue" },
                new Categories { Id = 2, Name = "Expenses" },
                new Categories { Id = 3, Name = "Assets" },
                new Categories { Id = 4, Name = "Liabilities" });

            modelBuilder.Entity<Sources>().HasData(
                new Sources { Id = 1, Name = "Bank Transfer" },
                new Sources { Id = 2, Name = "Credit Card" },
                new Sources { Id = 3, Name = "Cash" },
                new Sources { Id = 4, Name = "Check" });

            // Sample transaction dates
            var dateToday = DateTime.Parse("07/17/2025");
            var dateYesterday = DateTime.Parse("07/16/2025");
            var dateBefore = DateTime.Parse("07/15/2025");

            modelBuilder.Entity<Transactions>().HasData(
                // Today’s transactions
                new Transactions { Id = 1, Amount = 1.00M, Date = dateToday, Description = "Description 1", CategoryId = 1, SourceId = 1 },
                new Transactions { Id = 2, Amount = 2.00M, Date = dateToday, Description = "Description 2", CategoryId = 1, SourceId = 2 },
                new Transactions { Id = 3, Amount = 3.00M, Date = dateToday, Description = "Description 3", CategoryId = 2, SourceId = 1 },

                // Yesterday’s transactions
                new Transactions { Id = 4, Amount = 4.00M, Date = dateYesterday, Description = "Description 4", CategoryId = 2, SourceId = 2 },
                new Transactions { Id = 5, Amount = 5.00M, Date = dateYesterday, Description = "Description 5", CategoryId = 3, SourceId = 1 },
                new Transactions { Id = 6, Amount = 6.00M, Date = dateYesterday, Description = "Description 6", CategoryId = 3, SourceId = 2 },

                // Day-before-yesterday’s transactions
                new Transactions { Id = 7, Amount = 7.00M, Date = dateBefore, Description = "Description 7", CategoryId = 3, SourceId = 3 },
                new Transactions { Id = 8, Amount = 8.00M, Date = dateBefore, Description = "Description 8", CategoryId = 4, SourceId = 1 },
                new Transactions { Id = 9, Amount = 9.00M, Date = dateBefore, Description = "Description 9", CategoryId = 4, SourceId = 3 },
                new Transactions { Id = 10, Amount = 10.00M, Date = dateBefore, Description = "Description 10", CategoryId = 4, SourceId = 4 }
                );
        }
    }
}
