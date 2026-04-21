using Microsoft.EntityFrameworkCore;
using SecureOps.Application.Intefaces;
using SecureOps.Domain.Entities;

namespace SecureOps.Infrastructure.Repository
{
    /// <inheritdoc cref="IIncidentCategoryRepository" />
    public class IncidentCategoryRepository : IIncidentCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public IncidentCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<IncidentCategory?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<IncidentCategory>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IncidentCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<IncidentCategory>().ToArrayAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            return entity is not null;
        }
    }
}
