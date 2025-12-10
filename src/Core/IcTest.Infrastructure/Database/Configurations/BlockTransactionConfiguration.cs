using IcTest.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IcTest.Infrastructure.Database.Configurations
{
    public class BlockTransactionConfiguration : IEntityTypeConfiguration<BlockTransaction>
    {
        public void Configure(EntityTypeBuilder<BlockTransaction> builder)
        {
            builder.HasKey(appset => appset.Id);

            builder.Property(appset => appset.Id).ValueGeneratedOnAdd();
            builder.Property(appset => appset.Hash).IsRequired().HasMaxLength(512);

            builder.HasOne(bt => bt.BlockHash)
                .WithMany(bh => bh.Txids)
                .HasForeignKey(bt => bt.BlockHashId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(u => u.Hash).IsUnique(true);
        }
    }
}
