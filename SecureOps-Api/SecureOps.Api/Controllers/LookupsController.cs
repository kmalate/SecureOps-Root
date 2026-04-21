using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SecureOps.Application.Intefaces;
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
    }
}
