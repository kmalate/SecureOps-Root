using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureOps.Application.Interfaces;
using SecureOps.Application.DTO;

namespace SecureOps.Api.Controllers
{
    /// <summary>
    /// API controller that exposes CRUD operations for incidents.
    /// </summary>
    /// <remarks>
    /// Endpoints provided: GET all, GET by id, POST create, PUT update, DELETE.
    /// </remarks>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IncidentsController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        /// <summary>
        /// Initializes a new instance of <see cref="IncidentsController"/>.
        /// </summary>
        /// <param name="incidentService">The incident service.</param>
        public IncidentsController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        /// <summary>
        /// Retrieves all incidents.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of <see cref="IncidentDTO"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var items = await _incidentService.GetAllAsync(cancellationToken);
            return Ok(items);
        }

        /// <summary>
        /// Retrieves a single incident by identifier.
        /// </summary>
        /// <param name="id">The identifier of the incident.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="IncidentDTO"/> if found; otherwise 404.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IncidentDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _incidentService.GetByIdAsync(id, cancellationToken);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a new incident.
        /// </summary>
        /// <param name="dto">The incident to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created <see cref="IncidentDTO"/> with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<IncidentDTO>> Create([FromBody] IncidentDTO dto, CancellationToken cancellationToken)
        {
            var created = await _incidentService.AddAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing incident.
        /// </summary>
        /// <param name="id">The identifier of the incident to update.</param>
        /// <param name="dto">The updated values.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>204 No Content on success, 400 for bad request, 404 if not found.</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] IncidentDTO dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id) return BadRequest();

            try
            {
                await _incidentService.UpdateAsync(dto, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes an incident by identifier.
        /// </summary>
        /// <param name="id">The identifier to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>204 No Content on success, 404 if not found.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var exists = await _incidentService.ExistsAsync(id, cancellationToken);
            if (!exists) return NotFound();

            await _incidentService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
