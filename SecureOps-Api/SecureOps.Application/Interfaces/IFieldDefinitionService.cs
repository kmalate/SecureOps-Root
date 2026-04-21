using SecureOps.Application.DTO;

namespace SecureOps.Application.Interfaces
{
    public interface IFieldDefinitionService
    {
        /// <summary>
        /// Retrieves a <see cref="FieldDefinitionDTO"/> by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the field definition.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="FieldDefinitionDTO"/>.</returns>
        Task<FieldDefinitionDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all <see cref="FieldDefinitionDTO"/> records.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of all field definition DTOs.</returns>
        Task<IEnumerable<FieldDefinitionDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new field definition.
        /// </summary>
        /// <param name="dto">The DTO representing the field definition to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created <see cref="FieldDefinitionDTO"/>, including any generated values.</returns>
        Task<FieldDefinitionDTO> AddAsync(FieldDefinitionDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing field definition.
        /// </summary>
        /// <param name="dto">The DTO containing updated values. The DTO must include the identifier of the entity to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpdateAsync(FieldDefinitionDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a field definition by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the field definition to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether a field definition with the specified identifier exists.
        /// </summary>
        /// <param name="id">The identifier to check.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><c>true</c> if the entity exists; otherwise <c>false</c>.</returns>
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
