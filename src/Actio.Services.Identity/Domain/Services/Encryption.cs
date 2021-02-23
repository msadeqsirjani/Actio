using System.Security.Cryptography;

namespace Actio.Services.Identity.Domain.Services
{
    public class Encryption : IEncryption
    {
        private const int SaltSize = 40;
        private const int DeriveBytesIterationsCount = 10000;

        public string GetSalt()
        {
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return saltBytes.ToBase64String();
        }

        public string GetHash(string value, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);

            return pbkdf2.GetBytes(SaltSize).ToBase64String();
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];

            value.ToCharArray().BlockCopy(0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}