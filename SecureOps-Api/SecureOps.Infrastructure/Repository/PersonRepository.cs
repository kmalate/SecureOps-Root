using Microsoft.EntityFrameworkCore;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;

namespace SecureOps.Infrastructure.Repository
{
    /// <inheritdoc cref="IPersonRepository" />
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        /// <inheritdoc />
        public PersonRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<Person>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Set<Person>().ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Person> AddAsync(Person person, CancellationToken cancellationToken = default)
        {
            await _db.Set<Person>().AddAsync(person, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return person;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
        {
            _db.Set<Person>().Update(person);
            await _db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity is null) return;
            _db.Set<Person>().Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            return entity is not null;
        }
    }
}
