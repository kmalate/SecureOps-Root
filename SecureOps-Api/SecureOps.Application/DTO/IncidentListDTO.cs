using SecureOps.Domain.Entities;
using SecureOps.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureOps.Application.DTO
{
    public class IncidentListDTO
    {
        public Guid Id { get; set; }
        public IncidentCategoryDTO? Category { get; set; }
        public IncidentSeverityDTO? Severity { get; set; }
        public DateTime OccurredAt { get; set; }
        public EmployeeDTO? CreatedBy { get; set; }
        public EmployeeDTO? ReportedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public IncidentStatus Status { get; set; }
    }
}
