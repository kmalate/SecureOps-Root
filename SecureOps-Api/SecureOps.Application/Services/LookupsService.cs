
using SecureOps.Application.DTO;
using SecureOps.Application.Interfaces;

namespace SecureOps.Application.Services
{
    /// <inheritdoc />
    public class LookupsService : ILookupsService
    {
        private readonly IIncidentCategoryRepository _incidentCategoryRepository;
        private readonly IIncidentSeverityRepository _incidentSeverityRepository;
        private readonly IInvolvementTypeRepository _involvementTypeRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="LookupsService"/>.
        /// </summary>
        /// <param name="incidentCategoryRepository">Repository for incident categories.</param>
        /// <param name="incidentSeverityRepository">Repository for incident severities.</param>
        /// <param name="involvementTypeRepository">Repository for involvement types.</param>
        public LookupsService(
            IIncidentCategoryRepository incidentCategoryRepository,
            IIncidentSeverityRepository incidentSeverityRepository,
            IInvolvementTypeRepository involvementTypeRepository)
        {
            _incidentCategoryRepository = incidentCategoryRepository;
            _incidentSeverityRepository = incidentSeverityRepository;
            _involvementTypeRepository = involvementTypeRepository;
        }

        /// <inheritdoc />
        public async Task<IncidentCategoryDTO?> GetIncidentCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _incidentCategoryRepository.GetByIdAsync(id, cancellationToken);
            if (entity is null) return null;

            return new IncidentCategoryDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IncidentCategoryDTO>> GetAllIncidentCategoryAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _incidentCategoryRepository.GetAllAsync(cancellationToken);
            return entities.Select(e => new IncidentCategoryDTO
            {
                Id = e.Id,
                Name = e.Name
            }).ToArray();
        }

        /// <inheritdoc />
        public async Task<IncidentSeverityDTO?> GetIncidentSeverityByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _incidentSeverityRepository.GetByIdAsync(id, cancellationToken);
            if (entity is null) return null;

            return new IncidentSeverityDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IncidentSeverityDTO>> GetAllIncidentSeverityAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _incidentSeverityRepository.GetAllAsync(cancellationToken);
            return entities.Select(e => new IncidentSeverityDTO
            {
                Id = e.Id,
                Name = e.Name
            }).ToArray();
        }

        /// <inheritdoc />
        public async Task<InvolvementTypeDTO?> GetInvolvementTypeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _involvementTypeRepository.GetByIdAsync(id, cancellationToken);
            if (entity is null) return null;

            return new InvolvementTypeDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        /// <inheritdoc />
        public async Task<IEnumerable<InvolvementTypeDTO>> GetAllInvolvementTypesAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _involvementTypeRepository.GetAllAsync(cancellationToken);
            return entities.Select(e => new InvolvementTypeDTO
            {
                Id = e.Id,
                Name = e.Name
            }).ToArray();
        }
    }
}
