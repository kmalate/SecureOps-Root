using System;
using System.Collections.Generic;
using System.Text;

namespace SecureOps.Application.DTO
{
    public class FieldDefinitionListDTO
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public object Options { get; set; } = new { };
        public FieldTypeDTO? FieldType { get; set; }
        public int DisplayOrder { get; set; }
    }
}
