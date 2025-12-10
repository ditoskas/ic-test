using IcTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IcTest.Infrastructure.Database.Configurations
{
    public class BlockHashConfiguration : IEntityTypeConfiguration<BlockHash>
    {
        public void Configure(EntityTypeBuilder<BlockHash> builder)
        {
            builder.HasKey(appset => appset.Id);

            builder.Property(appset => appset.Id).ValueGeneratedOnAdd();
            builder.Property(appset => appset.Hash).IsRequired().HasMaxLength(512);
            builder.Property(appset => appset.Chain).IsRequired().HasMaxLength(256);
            builder.Property(appset => appset.Size).IsRequired(false);
            builder.Property(appset => appset.Vsize).IsRequired(false);
            builder.Property(appset => appset.CoinbaseAddr).IsRequired().HasMaxLength(512);
            builder.Property(appset => appset.RelayedBy).IsRequired().HasMaxLength(512);
            builder.Property(appset => appset.PrevBlock).IsRequired().HasMaxLength(512);
            builder.Property(appset => appset.MrklRoot).IsRequired().HasMaxLength(512);
            builder.Property(appset => appset.PrevBlockUrl).IsRequired().HasMaxLength(2048);
            builder.Property(appset => appset.TxUrl).IsRequired().HasMaxLength(2048);
            builder.Property(appset => appset.NextTxids).IsRequired(false).HasMaxLength(2048);

            builder.HasIndex(u => u.Hash).IsUnique(true);
        }
    }
}
