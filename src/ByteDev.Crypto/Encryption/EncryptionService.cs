using System;
using System.IO;
using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.Algorithms;
using ByteDev.Crypto.Encryption.KeyIv;

namespace ByteDev.Crypto.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IEncryptionAlgorithm _encryptionAlgorithm;
        private readonly EncryptionKeyIv _keyIv;

        public EncryptionService(IEncryptionAlgorithm encryptionAlgorithm, EncryptionKeyIv keyIv)
        {
            _encryptionAlgorithm = encryptionAlgorithm ?? throw new ArgumentNullException(nameof(encryptionAlgorithm));
            _keyIv = keyIv ?? throw new ArgumentNullException(nameof(keyIv));
        }

        public byte[] Encrypt(string clearText)
        {
            if(string.IsNullOrEmpty(clearText))
                throw new ArgumentException("Text must not be null or empty");

            var clearTextBytes = System.Text.Encoding.Default.GetBytes(clearText);

            var transform = _encryptionAlgorithm.CreateEncryptor(_keyIv);

            return TransformBytes(transform, clearTextBytes);
        }

        public string Decrypt(byte[] cipher)
        {
            if(cipher == null || cipher.Length < 1)
                throw new ArgumentException("Cipher was null or has zero elements");

            var transform = _encryptionAlgorithm.CreateDecryptor(_keyIv);

            var outputBytes = TransformBytes(transform, cipher);

            return System.Text.Encoding.Default.GetString(outputBytes);
        }

        private static byte[] TransformBytes(ICryptoTransform transform, byte[] bytes)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(outputStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                }
                return outputStream.ToArray();
            }
        }
    }
}
