namespace SecureOps.Application.DTO
{
    /// <summary>
    /// Data Transfer Object for Person entity.
    /// </summary>
    public class PersonDTO
    {
        /// <summary>
        /// The unique identifier of the person.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The first name of the person.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the person.
        /// </summary>
        public string LastName { get; set; } = string.Empty;
    }
}
