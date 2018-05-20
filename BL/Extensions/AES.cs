using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TP.BL.Extensions
{
    public class AES
    {
        private static string IV = "XFg2e5yzc3DAm8kQ0MKaNw==";
        private static string KEY = "sf7FW5B0YejRgcyuXQ0X1u99ePjpFcQVORJlVxr4irs=";

        private static string ByteToString(byte[] bytes) => Convert.ToBase64String(bytes);
        private static byte[] StringToByte(string code) => Convert.FromBase64String(code);

        public static string Encrypt(string text) => Encrypt(text, IV, KEY);
        public static string Encrypt(string text, string key) => Encrypt(text, IV, key);
        public static string Encrypt(string text, string iv, string key)
        {
            Aes aes = Aes.Create();
            aes.IV = StringToByte(iv);
            aes.Key = StringToByte(key);

            byte[] encrypted;
            ICryptoTransform crypt = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(text);
                    }
                }
                encrypted = ms.ToArray();
            }

            return ByteToString(encrypted);
        }

        public static string Decrypt(string hash) => Decrypt(hash, IV, KEY);
        public static string Decrypt(string hash, string key) => Decrypt(hash, IV, key);
        public static string Decrypt(string hash, string iv, string key)
        {
            Aes aes = Aes.Create();
            aes.IV = StringToByte(iv);
            aes.Key = StringToByte(key);

            string text = "";
            byte[] data = StringToByte(hash);
            ICryptoTransform crypt = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        text = sr.ReadToEnd();
                    }
                }
            }
            return text;
        }
    }
}
