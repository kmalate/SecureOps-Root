
using Microsoft.EntityFrameworkCore;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;

namespace SecureOps.Infrastructure.Repository
{
    /// <inheritdoc cref="IIncidentSeverityRepository" />
    public class IncidentSeverityRepository: IIncidentSeverityRepository
    {
        private readonly ApplicationDbContext _db;

        public IncidentSeverityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<IncidentSeverity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<IncidentSeverity>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IncidentSeverity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<IncidentSeverity>().ToArrayAsync(cancellationToken);
        }
    }
}
