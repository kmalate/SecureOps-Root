
using SecureOps.Domain.Entities;

namespace SecureOps.Application.Interfaces
{
    /// <summary>
    /// Repository abstraction for reading involvement type data.
    /// This interface exposes read-only operations.
    /// </summary>
    public interface IInvolvementTypeRepository
    {
        /// <summary>
        /// Retrieves an <see cref="InvolvementType"/> by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the involvement type.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="InvolvementType"/>, or <c>null</c> if not found.</returns>
        Task<InvolvementType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all <see cref="InvolvementType"/> records.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An enumerable of all involvement types.</returns>
        Task<IEnumerable<InvolvementType>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
