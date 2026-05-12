using SecureOps.Application.DTO;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;
using System.Security.Claims;

namespace SecureOps.Application.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _repository;
        private readonly ClaimsPrincipal _user;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public IncidentService(IIncidentRepository repository, ClaimsPrincipal user)
        {
            _repository = repository;
            _user = user;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IncidentDTO> AddAsync(IncidentDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);
            var userId = _user?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("User context is not available.");

            var entity = new Incident
            {
                IncidentCategoryId = dto.IncidentCategoryId,
                IncidentSeverityId = dto.IncidentSeverityId,
                OccurredAt = dto.OccurredAt,
                Narrative = dto.Narrative,
                CreatedById = int.Parse(_user?.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                CreatedAt = DateTime.UtcNow,
                ReportedById = dto.ReportedById,
                CaseNumber = dto.CaseNumber,
                Metadata = dto.Metadata,
                Status = dto.Status
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
        public async Task<IEnumerable<IncidentListDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return entities.Select(item => new IncidentListDTO
            {
                Id = item.Id,
                Category = new IncidentCategoryDTO { Id = item.Category.Id, Name = item.Category.Name },
                Severity = new IncidentSeverityDTO { Id = item.Severity.Id, Name = item.Severity.Name },
                OccurredAt = item.OccurredAt,
                CreatedBy = MapEmployeeToDTO(item.CreatedBy),
                ReportedBy = MapEmployeeToDTO(item.ReportedBy),
                CreatedAt = item.CreatedAt,
                Status = item.Status
            }).ToList();

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
            entity.IncidentSeverityId = dto.IncidentSeverityId;
            entity.OccurredAt = dto.OccurredAt;
            entity.Narrative = dto.Narrative;
            entity.UpdatedById = int.Parse(_user?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            entity.UpdatedAt = DateTime.UtcNow;
            entity.ReportedById = dto.ReportedById;
            entity.CaseNumber = dto.CaseNumber;
            entity.Metadata = dto.Metadata;
            entity.Status = dto.Status;

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
        /// <inheritdoc />
        /// </summary>
        public async Task<IncidentParticipantDTO> AddParticipantAsync(IncidentParticipantDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var entity = new IncidentParticipant
            {
                IncidentId = dto.IncidentId,
                PersonId = dto.PersonId,
                InvolvementTypeId = dto.InvolvementTypeId
            };

            var added = await _repository.AddParticipantAsync(entity, cancellationToken);

            return MapParticipantToDTO(added);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task RemoveParticipantAsync(Guid incidentId, Guid personId, CancellationToken cancellationToken = default)
        {
            await _repository.RemoveParticipantAsync(incidentId, personId, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IEnumerable<IncidentParticipantDTO>> GetParticipantsByIncidentIdAsync(Guid incidentId, CancellationToken cancellationToken = default)
        {
            var participants = await _repository.GetParticipantsByIncidentIdAsync(incidentId, cancellationToken);
            return participants.Select(MapParticipantToDTO).ToList();
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
                IncidentSeverityId = entity.IncidentSeverityId,
                OccurredAt = entity.OccurredAt,
                Narrative = entity.Narrative,
                CreatedById = entity.CreatedById,
                ReportedById = entity.ReportedById,
                CaseNumber = entity.CaseNumber,
                Metadata = entity.Metadata,
                CreatedAt = entity.CreatedAt,
                Status = entity.Status,
                UpdatedById = entity.UpdatedById,
                UpdatedAt = entity.UpdatedAt
            };
        }

        /// <summary>
        /// Maps an IncidentParticipant entity to an IncidentParticipantDTO.
        /// </summary>
        private static IncidentParticipantDTO MapParticipantToDTO(IncidentParticipant entity)
        {
            return new IncidentParticipantDTO
            {
                IncidentId = entity.IncidentId,
                PersonId = entity.PersonId,
                InvolvementTypeId = entity.InvolvementTypeId
            };
        }

        private static EmployeeDTO MapEmployeeToDTO(Employee entity)
        {
            return new EmployeeDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                DateOfHire = entity.DateOfHire,
                IsRegistered = entity.IsRegistered,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
