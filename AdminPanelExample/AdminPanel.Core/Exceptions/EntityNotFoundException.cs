using AdminPanel.Core.Entities;

namespace AdminPanel.Core.Exceptions
{
    /// <summary>
    /// Exception for not founded entities
    /// </summary>
    /// <typeparam name="TEntity">Base-inherited entity</typeparam>
    public class EntityNotFoundException<TEntity>: Exception where TEntity : BaseEntity
    {
        public EntityNotFoundException(Guid id)
            : base($"Entity of type {typeof(TEntity)} with id {id} cannot be found.") { }

        public EntityNotFoundException(string name)
            : base($"Entity of type {typeof(TEntity)} with name {name} cannot be found.") { }
    }
}
