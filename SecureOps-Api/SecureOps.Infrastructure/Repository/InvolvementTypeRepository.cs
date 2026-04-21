
using Microsoft.EntityFrameworkCore;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;

namespace SecureOps.Infrastructure.Repository
{
    /// <inheritdoc cref="IInvolvementTypeRepository" />
    public class InvolvementTypeRepository : IInvolvementTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public InvolvementTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<InvolvementType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<InvolvementType>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<InvolvementType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<InvolvementType>().ToArrayAsync(cancellationToken);
        }
    }
}
