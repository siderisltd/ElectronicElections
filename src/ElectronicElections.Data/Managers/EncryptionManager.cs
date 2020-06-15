using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace ElectronicElections.Data.Managers
{
    public class EncryptionManager
    {
        private readonly SecureString k;

        public EncryptionManager(IConfiguration configuration)
        {
            var p1 = configuration["EM:p1"];
            var p2 = configuration["NA:p2"];
            var rotationPart = configuration["RP:rp"];
            var k1 = $"{p1}{rotationPart}{p2}";
            k1 = "b14ca5898a4e4133bbce2ea2315a1916";

            this.k = new NetworkCredential(string.Empty, k1).SecurePassword;
        }

        public string EncryptString(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(new NetworkCredential(string.Empty, this.k).Password);
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public string DecryptString(SecureString key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(new NetworkCredential(string.Empty, this.k).Password);
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new MemoryStream(buffer);
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }

    }
}
