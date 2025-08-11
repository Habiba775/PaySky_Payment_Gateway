using Domain.Entities;
using Domain.Entities.Custom_Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore.Metadata;

namespace InfraStructure.Persistence
{
    public class AppDbContext : IdentityDbContext<APPUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<CurrencyExchangeRate> CurrencyExchangeRates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().UseTpcMappingStrategy();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);





            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Transaction)
                .WithOne(t => t.Receipt)
                .HasForeignKey<Receipt>(r => r.TransactionId);

            modelBuilder.Entity<CheckingAccount>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.CheckingAccount)
                .HasForeignKey(t => t.CheckingAccountId);

            modelBuilder.Entity<SavingsAccount>()
                .HasMany(s => s.Transactions)
                .WithOne(t => t.SavingsAccount)
                .HasForeignKey(t => t.SavingsAccountId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        /* modelBuilder.Entity<SavingsAccount>()
             .HasOne(s => s.User)
             .WithMany(u => u.SavingsAccounts)
             .HasForeignKey(s => s.UserId)
             .OnDelete(DeleteBehavior.Cascade);

         builder.Entity<CheckingAccount>()
             .HasOne(c => c.User)
             .WithMany(u => u.CheckingAccounts)
             .HasForeignKey(c => c.UserId)
             .OnDelete(DeleteBehavior.Cascade);*/
    }


}

