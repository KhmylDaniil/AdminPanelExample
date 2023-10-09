using AdminPanel.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.DAL.Configurations
{
    /// <summary>
    /// Db entity configuration for <see cref="User"/> 
    /// </summary>
    internal class UserConfiguration : BaseEntityConfiguration<User>
    {
        protected override void ConfigureChild(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Age).IsRequired();
            builder.Property(x => x.Password).IsRequired();

            builder.Property(x => x.Email).IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity("UserRole")
                .ToTable("UserRoles");
        }
    }
}
