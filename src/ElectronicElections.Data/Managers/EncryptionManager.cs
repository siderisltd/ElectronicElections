using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace ElectronicElections.Data.Managers
{
    public class EncryptionManager : IDisposable
    {
        private readonly SecureString k;

        public EncryptionManager(EncryptionManagerConfiguration config)
        {
            this.k = config.K1;
            this.k.MakeReadOnly();
        }

        internal string EncryptString(string plainText)
        {
            using var aes = Aes.Create();
            var encryptor = aes.CreateEncryptor(Encoding.UTF8.GetBytes(new NetworkCredential(string.Empty, this.k).Password), new byte[16]);

            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            using var streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(plainText);

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        internal string DecryptString(SecureString key, string cipherText)
        {
            using var aes = Aes.Create();
            var decryptor = aes.CreateDecryptor(Encoding.UTF8.GetBytes(new NetworkCredential(string.Empty, this.k).Password), new byte[16]);

            using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }

        public void Dispose()
        {
            this.k.Dispose();
        }
    }

    public class EncryptionManagerConfiguration : IDisposable
    {
        internal SecureString K1 { get; set; }

        public EncryptionManagerConfiguration(IConfiguration configuration)
        {
            var p1 = configuration["EM:p1"];
            var p2 = configuration["NA:p2"];
            var rotationPart = configuration["RP:rp"];
            var k1 = $"{p1}{rotationPart}{p2}";
            this.K1 = new NetworkCredential(string.Empty, k1).SecurePassword;
            this.K1.MakeReadOnly();
        }

        public void Dispose()
        {
            this.K1.Dispose();
        }
    }
}
