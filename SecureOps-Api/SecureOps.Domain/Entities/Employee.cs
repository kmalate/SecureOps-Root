using Microsoft.AspNetCore.Identity;

namespace SecureOps.Domain.Entities
{
    public class Employee : IdentityUser<int>
    {
        public Employee() : base() { }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public DateOnly DateOfHire { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public EmployeeVerification? EmployeeVerification { get; set; }
    }
}
