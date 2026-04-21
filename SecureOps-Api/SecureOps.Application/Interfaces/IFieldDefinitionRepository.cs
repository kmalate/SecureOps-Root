using SecureOps.Domain.Entities;


namespace SecureOps.Application.Interfaces
{
    public interface IFieldDefinitionRepository
    {
        /// <summary>
        /// Retrieves a <see cref="FieldDefinition"/> by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the field definition.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="FieldDefinition"/>, or <c>null</c> if not found.</returns>
        Task<FieldDefinition?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all <see cref="FieldDefinition"/> records.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of all field definitions.</returns>
        Task<IEnumerable<FieldDefinition>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new <see cref="FieldDefinition"/> to the data store.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The added entity, including any database-generated values.</returns>
        Task<FieldDefinition> AddAsync(FieldDefinition entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing <see cref="FieldDefinition"/>.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpdateAsync(FieldDefinition entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a <see cref="FieldDefinition"/> by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether a <see cref="FieldDefinition"/> with the specified identifier exists.
        /// </summary>
        /// <param name="id">The identifier to check.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><c>true</c> if the entity exists; otherwise <c>false</c>.</returns>
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
