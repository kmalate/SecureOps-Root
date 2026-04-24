using SecureOps.Application.DTO;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;

namespace SecureOps.Application.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _repository;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IncidentService(IIncidentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IncidentDTO> AddAsync(IncidentDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var entity = new Incident
            {
                IncidentCategoryId = dto.IncidentCategoryId,
                IncidentSeverityId = dto.Severity?.Id ?? 0,
                OccurredAt = dto.OccurredAt,
                Narrative = dto.Narrative,
                CreatedById = dto.CreatedById,
                ReportedById = dto.ReportedById,
                ReportedBy = null!,
                CaseNumber = dto.CaseNumber,
                Metadata = dto.Metadata
            };

            var added = await _repository.AddAsync(entity, cancellationToken);

            return MapToDTO(added);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IncidentDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return entity is null
                ? throw new KeyNotFoundException($"Incident with id {id} was not found.")
                : MapToDTO(entity);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IEnumerable<IncidentDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return entities.Select(MapToDTO).ToList();
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task UpdateAsync(IncidentDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken) 
                ?? throw new KeyNotFoundException($"Incident with id {dto.Id} was not found.");
            
            entity.IncidentCategoryId = dto.IncidentCategoryId;
            entity.IncidentSeverityId = dto.Severity?.Id ?? 0;
            entity.OccurredAt = dto.OccurredAt;
            entity.Narrative = dto.Narrative;
            entity.CreatedById = dto.CreatedById;
            entity.ReportedById = dto.ReportedById;
            entity.CaseNumber = dto.CaseNumber;
            entity.Metadata = dto.Metadata;

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
        /// Maps an Incident entity to an IncidentDTO.
        /// </summary>
        private static IncidentDTO MapToDTO(Incident entity)
        {
            return new IncidentDTO
            {
                Id = entity.Id,
                IncidentCategoryId = entity.IncidentCategoryId,
                Severity = entity.Severity,
                OccurredAt = entity.OccurredAt,
                Narrative = entity.Narrative,
                CreatedById = entity.CreatedById,
                ReportedById = entity.ReportedById,
                CaseNumber = entity.CaseNumber,
                Metadata = entity.Metadata,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
