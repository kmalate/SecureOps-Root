using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecureOps.Domain.Entities;
using System.Text.Json;

namespace SecureOps.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<Employee, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        { }

        public DbSet<EmployeeVerification> EmployeeVerifications { get; set; }
        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<FieldDefinition> FieldDefinitions { get; set; }
        public DbSet<IncidentCategory> IncidentCategories { get; set; }
        public DbSet<IncidentSeverity> IncidentSeverity { get; set; }
        public DbSet<InvolvementType> InvolvementTypes { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<IncidentParticipant> IncidentParticipants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Critical: Identity needs this

            // Your Shared Primary Key configuration
            modelBuilder.Entity<EmployeeVerification>()
                .HasKey(v => v.EmployeeId);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now() at time zone 'utc'")
                .HasConversion(
                    src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                    dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
                );

                entity.HasOne(e => e.EmployeeVerification)
                    .WithOne(v => v.Employee)
                    .HasForeignKey<EmployeeVerification>(v => v.EmployeeId);

            });

            var optionsConverter = new ValueConverter<object, string>(
                v => JsonSerializer.Serialize(v),
                v => JsonSerializer.Deserialize<object>(v) ?? new { }
            );

            modelBuilder.Entity<FieldDefinition>(entity =>
            {
                entity.Property(e => e.Label).IsRequired().HasMaxLength(50);
                //entity.Property(e => e.Options).HasConversion(optionsConverter).HasColumnType("jsonb");
                entity.Property(e => e.Options).HasColumnType("jsonb");
            });

            modelBuilder.Entity<IncidentCategory>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<IncidentSeverity>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<InvolvementType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.Property(e => e.Narrative).HasMaxLength(2000);
                entity.Property(e => e.CaseNumber).HasMaxLength(100);
                entity.Property(e => e.Metadata).HasColumnType("jsonb");
                entity.Property(e => e.OccurredAt)
                    .HasDefaultValueSql("now() at time zone 'utc'")
                    .HasConversion(
                        src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                        dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
                    );
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("now() at time zone 'utc'")
                    .HasConversion(
                        src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                        dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
                     );
            });

            modelBuilder.Entity<IncidentParticipant>(entity =>
            {
                modelBuilder.Entity<IncidentParticipant>()
                    .HasKey(ip => new { ip.IncidentId, ip.PersonId, ip.InvolvementTypeId });
                entity.Property(e => e.ParticipantData).HasColumnType("jsonb");
            });
        }
    }
}
