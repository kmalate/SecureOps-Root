using SecureOps.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SecureOps.Application.Intefaces
{
    /// <summary>
    /// Repository abstraction for reading incident category data.
    /// This interface exposes read-only operations only.
    /// </summary>
    public interface IIncidentCategoryRepository
    {
        /// <summary>
        /// Retrieves an <see cref="IncidentCategory"/> by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the incident category.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="IncidentCategory"/>, or <c>null</c> if not found.</returns>
        Task<IncidentCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all <see cref="IncidentCategory"/> records.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of all incident categories.</returns>
        Task<IEnumerable<IncidentCategory>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks whether an <see cref="IncidentCategory"/> with the specified identifier exists.
        /// </summary>
        /// <param name="id">The identifier to check.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><c>true</c> if the entity exists; otherwise <c>false</c>.</returns>
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
