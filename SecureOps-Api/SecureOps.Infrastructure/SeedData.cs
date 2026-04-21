using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecureOps.Domain.Entities;

namespace SecureOps.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
                );
            if (!context.FieldTypes.Any())
            {
                context.FieldTypes.AddRange(
                    new FieldType { Name = "Text" },
                    new FieldType { Name = "Number" },
                    new FieldType { Name = "Date" },
                    new FieldType { Name = "Boolean" },
                    new FieldType { Name = "Select" }
                );
            }

            if (!context.IncidentCategories.Any())
            {
                context.IncidentCategories.AddRange(
                    new IncidentCategory { Name = "Access & Perimeter Issues" },
                    new IncidentCategory { Name = "Crime & Property Damage" },
                    new IncidentCategory { Name = "Safety & Medical" },
                    new IncidentCategory { Name = "Disturbances" },
                    new IncidentCategory { Name = "Emergency Hazards" },
                    new IncidentCategory { Name = "Operational Issues" },
                    new IncidentCategory { Name = "Other" }
                );
            }

            if (!context.IncidentSeverity.Any())
            {
                context.IncidentSeverity.AddRange(
                    new IncidentSeverity { Name = "Low" },
                    new IncidentSeverity { Name = "Medium" },
                    new IncidentSeverity { Name = "High" },
                    new IncidentSeverity { Name = "Critical" },
                    new IncidentSeverity { Name = "Informational" }
                );
            }

            if (!context.InvolvementTypes.Any())
            {
                context.InvolvementTypes.AddRange(
                    new InvolvementType { Name = "Witness" },
                    new InvolvementType { Name = "Victim" },
                    new InvolvementType { Name = "Suspect" },
                    new InvolvementType { Name = "Complainant" },
                    new InvolvementType { Name = "Other" }
                );
            }

            context.SaveChanges();
        }
    }
}
