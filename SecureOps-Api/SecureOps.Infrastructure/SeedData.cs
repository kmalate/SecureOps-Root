using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecureOps.Domain.Entities;
using SecureOps.Domain.Enums;

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

            context.SaveChanges();

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

            if (!context.FieldDefinitions.Any())
            {
                context.FieldDefinitions.AddRange(
                    new FieldDefinition
                    {
                        Id = 5,
                        Label = "Facility Zone",
                        Target = FieldTarget.Incident,
                        FieldTypeId = 5, // Dropdown
                        Options = "[\"Parking Lot\", \"Lobby\", \"Warehouse\", \"Retail Floor\", \"Office\", \"Loading Dock\"]",
                        DisplayOrder = 1
                    },
                    new FieldDefinition
                    {
                        Id = 6,
                        Label = "Street Address",
                        Target = FieldTarget.Incident,
                        FieldTypeId = 1, // Text
                        Options = "{}",
                        DisplayOrder = 2
                    },
                    new FieldDefinition
                    {
                        Id = 7,
                        Label = "GPS Coordinates",
                        Target = FieldTarget.Incident,
                        FieldTypeId = 1, // Text (or specialized geo field)
                        Options = "{\"placeholder\": \"Lat, Long\"}",
                        DisplayOrder = 3
                    },

                    // --- ASSET & IMPACT ---
                    new FieldDefinition
                    {
                        Id = 8,
                        Label = "Property Damage Involved",
                        Target = FieldTarget.Incident,
                        FieldTypeId = 4, // Boolean
                        Options = "{}",
                        DisplayOrder = 4
                    },
                    new FieldDefinition
                    {
                        Id = 9,
                        Label = "Asset Tag Number",
                        Target = FieldTarget.Incident,
                        FieldTypeId = 1, // Text
                        Options = "{\"pattern\": \"^[A-Z]{2}-\\\\d{5}$\"}", // Optional Regex
                        DisplayOrder = 5
                    },
                    new FieldDefinition
                    {
                        Id = 12,
                        Label = "CCTV Footage Available",
                        Target = FieldTarget.Incident,
                        FieldTypeId = 4, // Boolean
                        Options = "{}",
                        DisplayOrder = 6
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
