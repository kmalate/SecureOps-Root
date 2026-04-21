

using System.ComponentModel.DataAnnotations;

namespace SecureOps.Domain.Entities
{
    public class InvolvementType
    {
        public int  Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
