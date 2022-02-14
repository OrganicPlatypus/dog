using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace DomesticOrganizationGuru.Api.Application.Services.Implementation.Hashing
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32;

        public PasswordHasher(IOptions<HashingOptions> options)
        {
            Options = options.Value;
        }

        private HashingOptions Options { get; }

        public string Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Options.Iterations,
              HashAlgorithmName.SHA512);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return string.Format($"{Options.Iterations}.{salt}.{key}");
        }

        public bool  Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA512);
            var keyToCheck = algorithm.GetBytes(KeySize);

            bool isValid = keyToCheck.SequenceEqual(key);

            if (!isValid)
            {
                throw new UnauthorizedAccessException("Password is not valid");
            }
            return isValid;
        }
    }
}
