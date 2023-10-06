using AdminPanel.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Core;

namespace AdminPanel.DAL.Configurations
{
    /// <summary>
    /// Db entity configuration for <see cref="Role"/> 
    /// </summary>
    internal class RoleConfiguration : BaseEntityConfiguration<Role>
    {
        protected override void ConfigureChild(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(x => x.Users)
                .WithMany(x => x.Roles)
                .UsingEntity("UserRole")
                .ToTable("UserRoles");

            builder.HasData(new Role
            {
                Id = Constants.SuperAdminRoleId,
                Name = Constants.SuperAdminRoleName
            });

            builder.HasData(new Role
            {
                Id = Constants.AdminRoleId,
                Name = Constants.AdminRoleName
            });

            builder.HasData(new Role
            {
                Id = Constants.UserRoleId,
                Name = Constants.UserRoleName
            });

            builder.HasData(new Role
            {
                Id = Constants.SupportRoleId,
                Name = Constants.SupportRoleName
            });
        }
    }
}
