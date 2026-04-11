using Microsoft.EntityFrameworkCore;
using SecureOps.Application;
using SecureOps.Application.Intefaces;
using SecureOps.Domain.Entities;
using SecureOps.Infrastructure;
using SecureOps.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'WebApplication1Context' not found.")));

//Add Identity services to the container
builder.Services.AddAuthorization();
// Add services from the other projects
builder.Services.AddScoped<IFieldDefinitionRepository, FieldDefinitionRepository>();
builder.Services.AddApplicationServices(); // From Application project

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

app.MapIdentityApi<Employee>();

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
