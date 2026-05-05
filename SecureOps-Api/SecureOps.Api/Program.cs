using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using SecureOps.Application;
using SecureOps.Application.Interfaces;
using SecureOps.Application.Validators;
using SecureOps.Domain.Entities;
using SecureOps.Infrastructure;
using SecureOps.Infrastructure.Repository;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
// Enables dynamic JSON mapping for System.Text.Json
dataSourceBuilder.EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(dataSource));

// Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");

var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT configuration 'Jwt:Issuer' is missing.");
var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT configuration 'Jwt:Audience' is missing.");
var keyString = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT configuration 'Jwt:Key' is missing.");
var key = Encoding.UTF8.GetBytes(keyString);

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,

        ValidateAudience = true,
        ValidAudience = audience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ClockSkew = TimeSpan.Zero // Removes the default 5-minute grace period for expiry
    };
});

//Add Identity services to the container
builder.Services.AddAuthorization();
// Add services from the other projects
builder.Services.AddScoped<IFieldDefinitionRepository, FieldDefinitionRepository>();
builder.Services.AddScoped<IIncidentCategoryRepository, IncidentCategoryRepository>();
builder.Services.AddScoped<IIncidentSeverityRepository, IncidentSeverityRepository>();
builder.Services.AddScoped<IInvolvementTypeRepository, InvolvementTypeRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddApplicationServices(); // From Application project
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ClaimsPrincipal>(s =>
    s.GetRequiredService<IHttpContextAccessor>().HttpContext?.User ?? new ClaimsPrincipal());
builder.Services.AddValidatorsFromAssemblyContaining<IncidentDtoValidator>();

builder.Services.AddIdentityApiEndpoints<Employee>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
const string secureOpsClientOrigins = "_secureOpsClientOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: secureOpsClientOrigins,
                      policy =>
                      {
                          //TODO: move this to configuration
                          policy.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

//app.MapIdentityApi<Employee>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(secureOpsClientOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
