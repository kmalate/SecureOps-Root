using SecureOps.Domain.Entities;
using SecureOps.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureOps.Application.DTO
{
    public class IncidentDTO
    {
        public Guid Id { get; set; }
        public int IncidentCategoryId { get; set; }
        public int IncidentSeverityId { get; set; }
        public DateTime OccurredAt { get; set; }
        public int CreatedById { get; set; }
        public string Narrative { get; set; } = string.Empty;
        public int ReportedById { get; set; }
        public string CaseNumber { get; set; } = string.Empty;
        public Dictionary<string, object> Metadata { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public IncidentStatus Status { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
