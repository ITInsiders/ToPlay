using System;
using System.Security.Cryptography;
using System.Text;

namespace TP.BL.Extensions
{
    public class Hash
    {
        public enum TypeHash
        {
            SHA256, SHA384, SHA512
        }

        private static int minSaltLength = 4, maxSaltLength = 16;

        public static byte[] GenerateSalt()
        {
            Random r = new Random();
            int SaltLength = r.Next(minSaltLength, maxSaltLength);
            byte[] salt = new byte[SaltLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(salt);
            rng.Dispose();
            return salt;
        }

        public static byte[] GenerateSalt(string salt)
        {
            if (salt == null || salt == "") return null;

            return ASCIIEncoding.UTF8.GetBytes(salt);
        }

        public static string GetHash(TypeHash type, string text, byte[] salt = null)
        {
            salt = salt ?? GenerateSalt();
            byte[] plain = ASCIIEncoding.UTF8.GetBytes(text);

            int lenghtPlant = plain.Length,
                lenghtSalt = salt.Length,
                lenght = lenghtPlant + lenghtSalt;

            byte[] bytes = new byte[lenght];
            for (int i = 0; i < lenght; ++i)
            {
                if (i < lenghtPlant) bytes[i] = plain[i];
                else bytes[i] = salt[i - lenghtPlant];
            }

            byte[] hash = null;
            switch(type)
            {
                case TypeHash.SHA256:
                    SHA256 sha256 = SHA256Managed.Create();
                    hash = sha256.ComputeHash(bytes);
                    sha256.Dispose();
                    break;
                case TypeHash.SHA384:
                    SHA384 sha384 = SHA384Managed.Create();
                    hash = sha384.ComputeHash(bytes);
                    sha384.Dispose();
                    break;
                case TypeHash.SHA512:
                    SHA512 sha512 = SHA512Managed.Create();
                    hash = sha512.ComputeHash(bytes);
                    sha512.Dispose();
                    break;
            }

            return GetStringFromHash(hash);
        }

        public static bool Confirm(TypeHash type, string text, string hash, byte[] salt = null)
        {
            return hash == GetHash(type, text, salt);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

    }
}
