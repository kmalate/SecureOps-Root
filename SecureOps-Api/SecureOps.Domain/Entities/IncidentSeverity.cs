

using System.ComponentModel.DataAnnotations;

namespace SecureOps.Domain.Entities
{
    public class IncidentSeverity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
