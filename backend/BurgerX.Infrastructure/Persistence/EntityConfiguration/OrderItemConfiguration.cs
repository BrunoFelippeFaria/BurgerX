using BurgerX.Domain.Entities.Orders;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurgerX.Infrastructure.Persistence.EntityConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(p => p.Id);
        builder.HasQueryFilter(p => p.IsDeleted == false);

        builder.HasOne(o => o.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(o => o.OrderId);

        builder.HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId);
    }
}