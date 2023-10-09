using AdminPanel.Core.Entities;

namespace AdminPanel.Core.Exceptions
{
    /// <summary>
    /// Exception for not founded entities
    /// </summary>
    /// <typeparam name="TEntity">Base-inherited entity</typeparam>
    public class EntityNotFoundException<TEntity>: Exception where TEntity : BaseEntity
    {
        /// <summary>
        /// ctor for <see cref="EntityNotFoundException{TEntity}"/>
        /// </summary>
        /// <param name="id">Entity id</param>
        public EntityNotFoundException(Guid id)
            : base($"Entity of type {typeof(TEntity).Name} with id {id} cannot be found.") { }

        /// <summary>
        /// ctor for <see cref="EntityNotFoundException{TEntity}"/>
        /// </summary>
        /// <param name="name">Entity name</param>
        public EntityNotFoundException(string name)
            : base($"Entity of type {typeof(TEntity).Name} with name {name} cannot be found.") { }
    }
}
