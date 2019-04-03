using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Recepti.Helpers
{
    public class HashingPasswords
    {
        public static string GenerateHashArgon2(string password, string salt)
        {
            var pwBytes = Encoding.Unicode.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);

            var argon = new Argon2i(pwBytes)
            {
                DegreeOfParallelism = 16,
                MemorySize = 8192,
                Iterations = 40,
                Salt = saltBytes
            };

            var hashBytes = argon.GetBytes(128);
            var hash = Convert.ToBase64String(hashBytes);

            return hash;
        }

        public static string GenerateSalt(uint byteLength = 16)
        {
            var salt = new byte[byteLength];
            var rng = new RNGCryptoServiceProvider();

            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}
