

namespace SecureOps.Application.DTO
{
    public class IncidentParticipantDTO
    {
        public Guid IncidentId { get; set; }
        public Guid PersonId { get; set; }
        public int InvolvementTypeId { get; set; }
    }
}
