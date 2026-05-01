using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecureOps.Application.DTO;
using SecureOps.Application.Services;
using SecureOps.Domain.Entities;
using SecureOps.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecureOps.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        
        public AuthenticationController(UserManager<Employee> userManager, ApplicationDbContext context, IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
            _jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("Configuration value 'Jwt:Key' is missing.");
            _jwtIssuer = _config["Jwt:Issuer"] ?? throw new InvalidOperationException("Configuration value 'Jwt:Issuer' is missing.");
            _jwtAudience = _config["Jwt:Audience"] ?? throw new InvalidOperationException("Configuration value 'Jwt:Audience' is missing.");
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                // TODO: Add roles if you use them
                //var userRoles = await _userManager.GetRolesAsync(user);
                //foreach (var role in userRoles)
                //{
                //    authClaims.Add(new Claim(ClaimTypes.Role, role));
                //}

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

                var token = new JwtSecurityToken(
                    issuer: _jwtIssuer,
                    audience: _jwtAudience,
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
