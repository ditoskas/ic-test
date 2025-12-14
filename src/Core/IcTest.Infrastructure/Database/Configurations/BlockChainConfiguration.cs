using IcTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IcTest.Infrastructure.Database.Configurations
{
    public class BlockChainConfiguration : IEntityTypeConfiguration<BlockChain>
    {
        public void Configure(EntityTypeBuilder<BlockChain> builder)
        {
            builder.HasKey(appset => appset.Id);

            builder.Property(appset => appset.Id).ValueGeneratedOnAdd();
            builder.Property(appset => appset.Coin).IsRequired().HasMaxLength(128);
            builder.Property(appset => appset.Chain).IsRequired().HasMaxLength(128);

            builder.HasIndex(u => new { u.Coin, u.Chain }).IsUnique(true);
        }
    }
}
