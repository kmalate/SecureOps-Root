using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureOps.Application.Interfaces;
using SecureOps.Application.DTO;

namespace SecureOps.Api.Controllers
{
    /// <summary>
    /// API controller that exposes CRUD operations for persons.
    /// </summary>
    /// <remarks>
    /// Endpoints provided: GET all, GET by id, POST create, PUT update, DELETE.
    /// </remarks>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// Initializes a new instance of <see cref="PersonsController"/>.
        /// </summary>
        /// <param name="personService">The person service.</param>
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Retrieves all persons.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of <see cref="PersonDTO"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var items = await _personService.GetAllAsync(cancellationToken);
            return Ok(items);
        }

        /// <summary>
        /// Retrieves a single person by identifier.
        /// </summary>
        /// <param name="id">The identifier of the person.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="PersonDTO"/> if found; otherwise 404.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PersonDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var item = await _personService.GetByIdAsync(id, cancellationToken);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="dto">The person to create.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Created <see cref="PersonDTO"/> with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<PersonDTO>> Create([FromBody] PersonDTO dto, CancellationToken cancellationToken)
        {
            var created = await _personService.AddAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="id">The identifier of the person to update.</param>
        /// <param name="dto">The updated values.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>204 No Content on success, 400 for bad request, 404 if not found.</returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonDTO dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id) return BadRequest();

            try
            {
                await _personService.UpdateAsync(dto, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a person by identifier.
        /// </summary>
        /// <param name="id">The identifier to delete.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>204 No Content on success, 404 if not found.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var exists = await _personService.ExistsAsync(id, cancellationToken);
            if (!exists) return NotFound();

            await _personService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
