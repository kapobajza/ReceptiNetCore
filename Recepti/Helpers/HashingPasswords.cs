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
        // Default hash size = 128
        private static int HASH_SIZE = 128;
        private static int NUMBER_OF_ITERATIONS = 40;

        public static string GenerateHashArgon2(string password, string salt)
        {
            var pwBytes = Encoding.Unicode.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);

            var argon = new Argon2i(pwBytes)
            {
                DegreeOfParallelism = 16,
                MemorySize = 8192,
                Iterations = NUMBER_OF_ITERATIONS,
                Salt = saltBytes
            };

            var hashBytes = argon.GetBytes(HASH_SIZE);
            var hash = Convert.ToBase64String(hashBytes);

            return hash;
        }

        public static string GenerateHashPBKDF2(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, NUMBER_OF_ITERATIONS, HashAlgorithmName.SHA256);

            var hashBytes = pbkdf2.GetBytes(HASH_SIZE);
            var hash = Convert.ToBase64String(hashBytes);

            return hash;
        }

        public static string GenerateHashSHA1(string password, string salt)
        {
            using (var sha1 = SHA1.Create())
            {
                var pwBytes = Encoding.UTF8.GetBytes(password);
                var saltBytes = Convert.FromBase64String(salt);
                var hash = new byte[pwBytes.Length + saltBytes.Length];

                for (int i = 0; i < pwBytes.Length; i++)
                {
                    hash[i] = pwBytes[i];
                }

                for (int i = 0; i < saltBytes.Length; i++)
                {
                    hash[pwBytes.Length + i] = saltBytes[i];
                }

                var computed = Convert.ToBase64String(sha1.ComputeHash(hash));

                return computed;
            }
        }

        public static string GenerateHashSHA256(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var pwBytes = Encoding.UTF8.GetBytes(password);
                var saltBytes = Convert.FromBase64String(salt);
                var hash = new byte[pwBytes.Length + saltBytes.Length];

                for (int i = 0; i < pwBytes.Length; i++)
                {
                    hash[i] = pwBytes[i];
                }

                for (int i = 0; i < saltBytes.Length; i++)
                {
                    hash[pwBytes.Length + i] = saltBytes[i];
                }

                var computed = Convert.ToBase64String(sha256.ComputeHash(hash));

                return computed;
            }
        }

        public static string GenerateHashSHA512(string password, string salt)
        {
            using (var sha512 = SHA512.Create())
            {
                var pwBytes = Encoding.UTF8.GetBytes(password);
                var saltBytes = Convert.FromBase64String(salt);
                var hash = new byte[pwBytes.Length + saltBytes.Length];

                for (int i = 0; i < pwBytes.Length; i++)
                {
                    hash[i] = pwBytes[i];
                }

                for (int i = 0; i < saltBytes.Length; i++)
                {
                    hash[pwBytes.Length + i] = saltBytes[i];
                }

                var computed = Convert.ToBase64String(sha512.ComputeHash(hash));

                return computed;
            }
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
