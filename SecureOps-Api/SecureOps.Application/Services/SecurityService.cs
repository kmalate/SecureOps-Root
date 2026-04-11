using SecureOps.Application.Intefaces;
using System.Security.Cryptography;

namespace SecureOps.Application.Services
{
    public class SecurityService : ISecurityService
    {
        private const int SaltSize = 16; // 128 bit
        private const int HashSize = 32; // 256 bit
        private const int Iterations = 100000; // High iteration count for security
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;

        /// <inheritdoc />
        public (byte[] Hash, byte[] Salt) HashSSN(string ssnLastFour)
        {
            // Generate a cryptographically strong random salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Use the static Pbkdf2 method
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                ssnLastFour,
                salt,
                Iterations,
                _hashAlgorithm,
                HashSize);

            return (hash, salt);
        }

        /// <inheritdoc />
        public bool VerifySSN(string input, byte[] storedHash, byte[] storedSalt)
        {
            // Derive the hash from the input using the same salt and parameters
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                storedSalt,
                Iterations,
                _hashAlgorithm,
                HashSize);

            // Use FixedTimeEquals to prevent timing attacks
            return CryptographicOperations.FixedTimeEquals(inputHash, storedHash);
        }
    }
}
