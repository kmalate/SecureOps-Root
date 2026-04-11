namespace SecureOps.Application.DTO
{
    public class FieldDefinitionDTO
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public int FieldTypeId { get; set; }
        public object Options { get; set; } = new { };
    }
}
