using Microsoft.EntityFrameworkCore;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;

namespace SecureOps.Infrastructure.Repository
{
    /// <inheritdoc cref="IIncidentRepository" />
    public class IncidentRepository : IIncidentRepository
    {
        private readonly ApplicationDbContext _db;

        public IncidentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<Incident?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Incident>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Incident>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<Incident>().ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Incident> AddAsync(Incident incident, CancellationToken cancellationToken = default)
        {
            await _db.Set<Incident>().AddAsync(incident, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return incident;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Incident incident, CancellationToken cancellationToken = default)
        {
            _db.Set<Incident>().Update(incident);
            await _db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is null) return;

            _db.Set<Incident>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            return entity is not null;
        }

        /// <inheritdoc />
        public async Task<IncidentParticipant> AddParticipantAsync(IncidentParticipant participant, CancellationToken cancellationToken = default)
        {
            await _db.Set<IncidentParticipant>().AddAsync(participant, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return participant;
        }

        /// <inheritdoc />
        public async Task RemoveParticipantAsync(Guid incidentId, Guid personId, CancellationToken cancellationToken = default)
        {
            var participant = await _db.Set<IncidentParticipant>()
                .FirstOrDefaultAsync(p => p.IncidentId == incidentId && p.PersonId == personId, cancellationToken);
            
            if (participant is null) return;
            
            _db.Set<IncidentParticipant>().Remove(participant);
            await _db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IncidentParticipant>> GetParticipantsByIncidentIdAsync(Guid incidentId, CancellationToken cancellationToken = default)
        {
            return await _db.Set<IncidentParticipant>()
                .Where(p => p.IncidentId == incidentId)
                .ToListAsync(cancellationToken);
        }
    }
}
