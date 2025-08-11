using Domain.Entities;
using Domain.Entities.Custom_Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Persistence.Configurations
{
    public class SavingsAccountConfiguration : IEntityTypeConfiguration<SavingsAccount>
    {
        public void Configure(EntityTypeBuilder<SavingsAccount> builder)
        {
            builder.Property(s => s.InterestRate).HasColumnType("float");
            builder.Property(s => s.WithdrawalCount).IsRequired();
        }
    }
}



