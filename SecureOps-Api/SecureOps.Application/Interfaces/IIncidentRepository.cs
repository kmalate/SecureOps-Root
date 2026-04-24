
namespace SecureOps.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using SecureOps.Domain.Entities;

    public interface IIncidentRepository
    {
        /// <summary>
        /// Adds a new incident to the data store.
        /// </summary>
        /// <param name="incident">The incident to create.</param>
        /// <returns>The created incident, including any generated fields (for example, Id).</returns>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task<Incident> AddAsync(Incident incident, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an incident by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the incident.</param>
        /// <returns>The matching incident if found; otherwise null.</returns>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task<Incident?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all incidents.
        /// </summary>
        /// <returns>An enumerable containing all incidents.</returns>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task<IEnumerable<Incident>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing incident in the data store.
        /// </summary>
        /// <param name="incident">The incident instance containing updated values. The Id property identifies the record to update.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task UpdateAsync(Incident incident, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an incident by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the incident to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether an incident with the specified identifier exists.
        /// </summary>
        /// <param name="id">The unique identifier of the incident.</param>
        /// <returns>True if the incident exists; otherwise false.</returns>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
