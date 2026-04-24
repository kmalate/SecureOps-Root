using SecureOps.Application.DTO;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;

namespace SecureOps.Application.Services
{
    /// <inheritdoc cref="IPersonService" />
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<PersonDTO> AddAsync(PersonDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var entity = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var added = await _repository.AddAsync(entity, cancellationToken);

            return MapToDTO(added);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<PersonDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return entity is null
                ? throw new KeyNotFoundException($"Person with id {id} was not found.")
                : MapToDTO(entity);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IEnumerable<PersonDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return entities.Select(MapToDTO).ToList();
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task UpdateAsync(PersonDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Person with id {dto.Id} was not found.");

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.ExistsAsync(id, cancellationToken);
        }

        /// <summary>
        /// Maps a Person entity to a PersonDTO.
        /// </summary>
        /// <param name="entity">The Person entity to map.</param>
        /// <returns>The mapped PersonDTO.</returns>
        private static PersonDTO MapToDTO(Person entity)
        {
            return new PersonDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }
}
