using Microsoft.AspNetCore.Mvc;
using SecureOps.Application.Intefaces;
using SecureOps.Application.DTO;


namespace SecureOps.Api.Controllers
{
    /// <summary>
    /// API controller that exposes CRUD operations for field definitions.
    /// </summary>
    /// <remarks>
    /// Endpoints provided: GET all, GET by id, POST create, PUT update, DELETE.
    /// </remarks>
    [ApiController]
    [Route("[controller]")]
    public class FieldDefinitionController : ControllerBase
    {
        private readonly IFieldDefinitionService _service;

        /// <summary>
        /// Initializes a new instance of <see cref="FieldDefinitionController"/>.
        /// </summary>
        /// <param name="service">The field definition service.</param>
        public FieldDefinitionController(IFieldDefinitionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all field definitions.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of <see cref="FieldDefinitionDTO"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldDefinitionDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var items = await _service.GetAllAsync(cancellationToken);
            return Ok(items);
        }

        /// <summary>
        /// Retrieves a single field definition by identifier.
        /// </summary>
        /// <param name="id">The identifier of the field definition.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="FieldDefinitionDTO"/> if found; otherwise 404.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FieldDefinitionDTO>> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _service.GetByIdAsync(id, cancellationToken);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a new field definition.
        /// </summary>
        /// <param name="dto">The field definition to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created <see cref="FieldDefinitionDTO"/> with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<FieldDefinitionDTO>> Create([FromBody] FieldDefinitionDTO dto, CancellationToken cancellationToken)
        {
            var created = await _service.AddAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing field definition.
        /// </summary>
        /// <param name="id">The identifier of the field definition to update.</param>
        /// <param name="dto">The updated values.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>204 No Content on success, 400 for bad request, 404 if not found.</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] FieldDefinitionDTO dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id) return BadRequest();

            try
            {
                await _service.UpdateAsync(dto, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a field definition by identifier.
        /// </summary>
        /// <param name="id">The identifier to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>204 No Content on success, 404 if not found.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var exists = await _service.ExistsAsync(id, cancellationToken);
            if (!exists) return NotFound();

            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
