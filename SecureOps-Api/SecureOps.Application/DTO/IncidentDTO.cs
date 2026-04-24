using SecureOps.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureOps.Application.DTO
{
    public class IncidentDTO
    {
        public Guid Id { get; set; }
        public int IncidentCategoryId { get; set; }
        public IncidentSeverity? Severity { get; set; }
        public DateTime OccurredAt { get; set; }
        public string Narrative { get; set; } = string.Empty;
        public int CreatedById { get; set; }
        public int ReportedById { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public Dictionary<string, object> Metadata { get; set; } = [];
        public DateTime CreatedAt { get; set; }
    }
}
