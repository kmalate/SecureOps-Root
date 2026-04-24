

using SecureOps.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureOps.Domain.Entities
{
    public class Incident
    {
        public Guid Id { get; set; }
        public int IncidentCategoryId { get; set; }
        [Required, ForeignKey("IncidentCategoryId")]
        public IncidentCategory? Category { get; set; }
        public int IncidentSeverityId { get; set; }
        [Required, ForeignKey("IncidentSeverityId")]
        public IncidentSeverity? Severity { get; set; }
        public DateTime OccurredAt { get; set; }
        public string Narrative { get; set; } = string.Empty;
        public int CreatedById { get; set; } 
        [Required,ForeignKey("CreatedById")]
        public Employee? CreatedBy { get; set; }
        public int ReportedById { get; set; }
        [Required,ForeignKey("ReportedById")]
        public Employee? ReportedBy { get; set; }
        public ICollection<IncidentParticipant> Subjects { get; set; } = [];
        public string CaseNumber { get; set; } = string.Empty;
        public Dictionary<string, object> Metadata { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public IncidentStatus Status { get; set; }
    }
}
