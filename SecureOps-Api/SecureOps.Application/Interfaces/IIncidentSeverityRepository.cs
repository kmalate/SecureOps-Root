
using SecureOps.Domain.Entities;

namespace SecureOps.Application.Interfaces
{
    /// <summary>
    /// Repository abstraction for reading incident severity data.
    /// This interface exposes read-only operations.
    /// </summary>
    public interface IIncidentSeverityRepository
    {
        /// <summary>
        /// Retrieves an <see cref="IncidentSeverity"/> by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the incident severity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="IncidentSeverity"/>, or <c>null</c> if not found.</returns>
        Task<IncidentSeverity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all <see cref="IncidentSeverity"/> records.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of all incident severities.</returns>
        Task<IEnumerable<IncidentSeverity>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
