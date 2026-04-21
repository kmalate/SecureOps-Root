
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureOps.Domain.Entities
{
    public class IncidentParticipant
    {
        public Guid IncidentId { get; set; }
        [ForeignKey("IncidentId")]
        public Incident? Incident { get; set; }
        public Guid PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person? Person { get; set; }
        public int InvolvementTypeId { get; set; }
        [ForeignKey("InvolvementTypeId")]
        public InvolvementType? InvolvementType { get; set; }
        public Dictionary<string, object> ParticipantData { get; set; } = [];
    }
}
