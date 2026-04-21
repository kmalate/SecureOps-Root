using Microsoft.EntityFrameworkCore;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;

namespace SecureOps.Infrastructure.Repository
{
    /// <inheritdoc cref="IFieldDefinitionRepository" />
    public class FieldDefinitionRepository : IFieldDefinitionRepository
    {
        private readonly ApplicationDbContext _db;

        public FieldDefinitionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<FieldDefinition?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<FieldDefinition>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FieldDefinition>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<FieldDefinition>().ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<FieldDefinition> AddAsync(FieldDefinition entity, CancellationToken cancellationToken = default)
        {
            await _db.Set<FieldDefinition>().AddAsync(entity, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(FieldDefinition entity, CancellationToken cancellationToken = default)
        {
            _db.Set<FieldDefinition>().Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is null) return;

            _db.Set<FieldDefinition>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            return entity is not null;
        }
    }
}
