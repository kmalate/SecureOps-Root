using Microsoft.Extensions.DependencyInjection;
using SecureOps.Application.Intefaces;
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

            // If you use AutoMapper or FluentValidation later, add them here too
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
