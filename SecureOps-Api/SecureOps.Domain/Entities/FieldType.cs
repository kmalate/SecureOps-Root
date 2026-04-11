using System.ComponentModel.DataAnnotations;

namespace SecureOps.Domain.Entities
{
    public class FieldType
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string Name { get; set; } = string.Empty;
        public ICollection<FieldDefinition> FieldDefinitions { get; set; } = [];
    }
}
