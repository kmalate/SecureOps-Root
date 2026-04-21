

using System.ComponentModel.DataAnnotations;

namespace SecureOps.Domain.Entities
{
    public class IncidentCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Incident> Incidents { get; set; } = [];
    }
}
