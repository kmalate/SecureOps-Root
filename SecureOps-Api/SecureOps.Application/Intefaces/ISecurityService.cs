
namespace SecureOps.Application.Intefaces
{
    public interface ISecurityService
    {
        /// <summary>
        /// Generates a cryptographic hash and a unique salt for the provided last four digits of a Social Security
        /// Number (SSN).
        /// </summary>
        /// <remarks>Use the returned salt together with the hash for secure verification of the SSN
        /// segment. The method is intended to protect sensitive information by not storing the SSN in plain
        /// text.</remarks>
        /// <param name="ssnLastFour">The last four digits of the SSN to be hashed. Cannot be null or empty.</param>
        /// <returns>A tuple containing the hash and the salt as byte arrays. The hash is derived from the input and the
        /// generated salt.</returns>
        public (byte[] Hash, byte[] Salt) HashSSN(string ssnLastFour);

        /// <summary>
        /// Verifies whether the specified Social Security Number (SSN) matches the provided hash and salt values.
        /// </summary>
        /// <param name="input">The SSN to verify, as a string. Cannot be null.</param>
        /// <param name="storedHash">The hash value to compare against, as a byte array. Must not be null and should represent a valid hash of
        /// the SSN using the associated salt.</param>
        /// <param name="storedSalt">The salt value used when hashing the SSN, as a byte array. Must not be null and should match the salt used
        /// to generate the stored hash.</param>
        /// <returns>true if the SSN matches the stored hash and salt; otherwise, false.</returns>
        public bool VerifySSN(string input, byte[] storedHash, byte[] storedSalt);
    }
}
