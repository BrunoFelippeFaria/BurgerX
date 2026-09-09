using BurgerX.Domain.Administration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurgerX.Infrastructure.Persistence.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasQueryFilter(u => u.IsDeleted == false);
    }
}