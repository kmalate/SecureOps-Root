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
            if(context.FieldTypes.Any())
            {
                return;
            }

            context.FieldTypes.AddRange(
                new FieldType { Name = "Text" },
                new FieldType { Name = "Number" },
                new FieldType { Name = "Date" },
                new FieldType { Name = "Boolean" },
                new FieldType { Name = "Select" }
            );

            context.SaveChanges();
        }
    }
}
