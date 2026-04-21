using SecureOps.Application.DTO;

namespace SecureOps.Application.Interfaces
{
    /// <summary>
    /// Provides lookup operations for read-only reference data used across the application.
    /// </summary>
    public interface ILookupsService
    {
        /// <summary>
        /// Retrieves an incident category by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the incident category.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="IncidentCategoryDTO"/>, or <c>null</c> if not found.</returns>
        Task<IncidentCategoryDTO?> GetIncidentCategoryByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all incident categories.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of <see cref="IncidentCategoryDTO"/> records.</returns>
        Task<IEnumerable<IncidentCategoryDTO>> GetAllIncidentCategoryAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an incident severity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the incident severity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="IncidentSeverityDTO"/>, or <c>null</c> if not found.</returns>
        Task<IncidentSeverityDTO?> GetIncidentSeverityByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all incident severities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of <see cref="IncidentSeverityDTO"/> records.</returns>
        Task<IEnumerable<IncidentSeverityDTO>> GetAllIncidentSeverityAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an involvement type by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the involvement type.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="InvolvementTypeDTO"/>, or <c>null</c> if not found.</returns>
        Task<InvolvementTypeDTO?> GetInvolvementTypeByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all involvement types.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of <see cref="InvolvementTypeDTO"/> records.</returns>
        Task<IEnumerable<InvolvementTypeDTO>> GetAllInvolvementTypesAsync(CancellationToken cancellationToken = default);
    }
}