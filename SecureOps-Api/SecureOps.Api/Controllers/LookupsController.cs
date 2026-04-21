using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SecureOps.Application.Interfaces;
using SecureOps.Application.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace SecureOps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupsService _lookupsService;

        /// <summary>
        /// Initializes a new instance of <see cref="LookupsController"/>.
        /// </summary>
        /// <param name="lookupsService">Lookup service for reference data.</param>
        public LookupsController(ILookupsService lookupsService)
        {
            _lookupsService = lookupsService;
        }

        /// <summary>
        /// Retrieves all incident categories.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of incident category DTOs.</returns>
        [HttpGet("incident-categories")]
        public async Task<ActionResult<IEnumerable<IncidentCategoryDTO>>> GetIncidentCategories(CancellationToken cancellationToken)
        {
            var items = await _lookupsService.GetAllIncidentCategoryAsync(cancellationToken);
            return Ok(items);
        }

        /// <summary>
        /// Retrieves all incident severities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of incident severity DTOs.</returns>
        [HttpGet("incident-severities")]
        public async Task<ActionResult<IEnumerable<IncidentSeverityDTO>>> GetIncidentSeverities(CancellationToken cancellationToken)
        {
            var items = await _lookupsService.GetAllIncidentSeverityAsync(cancellationToken);
            return Ok(items);
        }

        /// <summary>
        /// Retrieves all involvement types.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of involvement type DTOs.</returns>
        [HttpGet("involvement-types")]
        public async Task<ActionResult<IEnumerable<InvolvementTypeDTO>>> GetInvolvementTypes(CancellationToken cancellationToken)
        {
            var items = await _lookupsService.GetAllInvolvementTypesAsync(cancellationToken);
            return Ok(items);
        }

        /// <summary>
        /// Retrieves a single incident category by id.
        /// </summary>
        /// <param name="id">Identifier of the incident category.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching incident category DTO or 404.</returns>
        [HttpGet("incident-categories/{id:int}")]
        public async Task<ActionResult<IncidentCategoryDTO>> GetIncidentCategoryById(int id, CancellationToken cancellationToken)
        {
            var item = await _lookupsService.GetIncidentCategoryByIdAsync(id, cancellationToken);
            if (item is null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Retrieves a single incident severity by id.
        /// </summary>
        /// <param name="id">Identifier of the incident severity.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching incident severity DTO or 404.</returns>
        [HttpGet("incident-severities/{id:int}")]
        public async Task<ActionResult<IncidentSeverityDTO>> GetIncidentSeverityById(int id, CancellationToken cancellationToken)
        {
            var item = await _lookupsService.GetIncidentSeverityByIdAsync(id, cancellationToken);
            if (item is null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Retrieves a single involvement type by id.
        /// </summary>
        /// <param name="id">Identifier of the involvement type.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching involvement type DTO or 404.</returns>
        [HttpGet("involvement-types/{id:int}")]
        public async Task<ActionResult<InvolvementTypeDTO>> GetInvolvementTypeById(int id, CancellationToken cancellationToken)
        {
            var item = await _lookupsService.GetInvolvementTypeByIdAsync(id, cancellationToken);
            if (item is null) return NotFound();
            return Ok(item);
        }
    }
}
