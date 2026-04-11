using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecureOps.Application.DTO;
using SecureOps.Application.Services;
using SecureOps.Domain.Entities;
using SecureOps.Infrastructure;

namespace SecureOps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ApplicationDbContext _context;
        
        public AuthenticationController(UserManager<Employee> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var security = new SecurityService();
            var (hash, salt) = security.HashSSN(model.SSNLastFour);

            var employee = new Employee
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmployeeVerification = new EmployeeVerification
                {
                    SSNLastFourHash = hash,
                    Salt = salt
                }
            };

            var result = await _userManager.CreateAsync(employee, model.Password);

            if (result.Succeeded) return Ok("Employee created with security credentials.");
            return BadRequest(result.Errors);
        }
    }
}
