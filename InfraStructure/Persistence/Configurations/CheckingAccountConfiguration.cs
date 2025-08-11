using Domain.Entities;
using Domain.Entities.Custom_Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Persistence.Configurations
{
    public class CheckingAccountConfiguration : IEntityTypeConfiguration<CheckingAccount>
    {
        public void Configure(EntityTypeBuilder<CheckingAccount> builder)
        {
            builder.Property(c => c.OverdraftLimit).HasColumnType("decimal(18,2)");
        }
    }
}

