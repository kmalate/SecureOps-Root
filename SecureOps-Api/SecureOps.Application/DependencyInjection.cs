using Microsoft.Extensions.DependencyInjection;
using SecureOps.Application.Interfaces;
using SecureOps.Application.Services;

namespace SecureOps.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IFieldDefinitionService, FieldDefinitionService>();
            services.AddScoped<ILookupsService, LookupsService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<IPersonService, PersonService>();

            // If you use AutoMapper or FluentValidation later, add them here too
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
