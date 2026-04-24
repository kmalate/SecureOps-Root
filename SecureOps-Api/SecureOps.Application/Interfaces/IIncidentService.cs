using SecureOps.Application.DTO;

namespace SecureOps.Application.Interfaces
{
    public interface IIncidentService
    {
        /// <summary>
        /// Retrieves an <see cref="IncidentDTO"/> by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the incident.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="IncidentDTO"/>.</returns>
        Task<IncidentDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all <see cref="IncidentDTO"/> records.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of all incident DTOs.</returns>
        Task<IEnumerable<IncidentDTO>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new incident.
        /// </summary>
        /// <param name="dto">The DTO representing the incident to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created <see cref="IncidentDTO"/>, including any generated values.</returns>
        Task<IncidentDTO> AddAsync(IncidentDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing incident.
        /// </summary>
        /// <param name="dto">The DTO containing updated values. The DTO must include the identifier of the entity to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpdateAsync(IncidentDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an incident by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the incident to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether an incident with the specified identifier exists.
        /// </summary>
        /// <param name="id">The identifier to check.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><c>true</c> if the incident exists; otherwise <c>false</c>.</returns>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a participant to an incident.
        /// </summary>
        /// <param name="dto">The incident participant DTO to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created <see cref="IncidentParticipantDTO"/>.</returns>
        Task<IncidentParticipantDTO> AddParticipantAsync(IncidentParticipantDTO dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes a participant from an incident.
        /// </summary>
        /// <param name="incidentId">The incident identifier.</param>
        /// <param name="personId">The person identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task RemoveParticipantAsync(Guid incidentId, Guid personId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all participants for a specific incident.
        /// </summary>
        /// <param name="incidentId">The incident identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of incident participant DTOs.</returns>
        Task<IEnumerable<IncidentParticipantDTO>> GetParticipantsByIncidentIdAsync(Guid incidentId, CancellationToken cancellationToken = default);
    }
}
