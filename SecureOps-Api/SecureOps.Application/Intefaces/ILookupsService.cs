using SecureOps.Application.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SecureOps.Application.Intefaces
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
    }
}
