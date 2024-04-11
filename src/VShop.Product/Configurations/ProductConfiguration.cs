using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShopProduct.Entities;

namespace VShopProduct.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price).HasPrecision(18, 5);

        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();

        builder.Property(x => x.Descripton).HasMaxLength(255).IsRequired();

        builder.Property(x => x.ImageUrl).HasMaxLength(255).IsRequired();
    }
}
