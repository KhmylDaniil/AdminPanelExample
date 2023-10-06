using AdminPanel.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.DAL.Configurations
{
    /// <summary>
    /// Id base configuration for all entities
    /// </summary>
    internal abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            ConfigureChild(builder);
        }

        /// <summary>
        /// Generic configuration method for inherited classes
        /// </summary>
        /// <typeparam name="TEntity">Inherited entity</typeparam>
        /// <param name="builder">Inherited entity builder</param>
        protected abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);
    }
}
