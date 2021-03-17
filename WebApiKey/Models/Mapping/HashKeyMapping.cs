using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApiKey.Models.Mapping
{
    public class HashKeyMapping : IEntityTypeConfiguration<HashKey>
    {
        public void Configure(EntityTypeBuilder<HashKey> builder)
        {
            builder.ToTable("keys");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Hash).HasColumnName("hash").HasMaxLength(100).IsRequired();

            builder.HasData(new[]
            {
                new HashKey{ Id= 1, Hash = "5dd5040e98674790abdb35f3af3bd004"},
                new HashKey{ Id = 2,Hash = "45e4f2e6f64f35f8b5418354155465e6"},
            });
        }
    }
}
