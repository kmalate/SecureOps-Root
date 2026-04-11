namespace SecureOps.Domain.Entities
{
    public class EmployeeVerification
    {
        public int EmployeeId { get; set; }
        public byte[] SSNLastFourHash { get; set; } = [];
        public byte[] Salt { get; set; } = [];
        public Employee? Employee { get; set; }
    }
}
