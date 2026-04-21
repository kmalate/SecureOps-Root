
using SecureOps.Application.Intefaces;
using SecureOps.Application.DTO;
using SecureOps.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecureOps.Application.Services
{
    /// <inheritdoc />
    public class LookupsService : ILookupsService
    {
        private readonly IIncidentCategoryRepository _incidentCategoryRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="LookupsService"/>.
        /// </summary>
        /// <param name="incidentCategoryRepository">Repository for incident categories.</param>
        public LookupsService(IIncidentCategoryRepository incidentCategoryRepository)
        {
            _incidentCategoryRepository = incidentCategoryRepository;
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
    }
}
