using SecureOps.Application.DTO;

namespace SecureOps.Application.Interfaces
{
    /// <summary>
    /// Service interface for managing Person entities with CRUD operations.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Retrieves a Person by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>The matching PersonDTO if found.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the person is not found.</exception>
        Task<PersonDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all Person entities.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An enumerable containing all persons as PersonDTO.</returns>
        Task<IEnumerable<PersonDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new Person to the data store.
        /// </summary>
        /// <param name="dto">The PersonDTO to create.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>The created PersonDTO, including any generated fields (for example, Id).</returns>
        Task<PersonDTO> AddAsync(PersonDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing Person in the data store.
        /// </summary>
        /// <param name="dto">The PersonDTO instance containing updated values. The Id property identifies the record to update.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the person is not found.</exception>
        Task UpdateAsync(PersonDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a Person by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the person to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether a Person with the specified identifier exists.
        /// </summary>
        /// <param name="id">The unique identifier of the person.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>True if the person exists; otherwise false.</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
