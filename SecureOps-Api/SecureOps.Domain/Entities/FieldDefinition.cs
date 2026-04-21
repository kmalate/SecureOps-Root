using SecureOps.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureOps.Domain.Entities
{
    public class FieldDefinition
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Label { get; set; } = string.Empty;
        public FieldTarget Target { get; set; }
        public int FieldTypeId { get; set; }
        [ForeignKey("FieldTypeId")]
        public FieldType? FieldType { get; set; }
        [Column(TypeName = "jsonb")]
        public string Options { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }   
    }
}
