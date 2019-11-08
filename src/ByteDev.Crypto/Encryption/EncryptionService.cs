using System;
using System.IO;
using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.Algorithms;
using ByteDev.Crypto.Encryption.KeyIv;

namespace ByteDev.Crypto.Encryption
{
    /// <summary>
    /// Represents a service for encrypting and decrypting. 
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private readonly IEncryptionAlgorithm _encryptionAlgorithm;
        private readonly EncryptionKeyIv _keyIv;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Encryption.EncryptionService" /> class.
        /// </summary>
        /// <param name="encryptionAlgorithm">Encryption algorithm to use when encrypting and decrypting.</param>
        /// <param name="keyIv">The key and initialization vector to use when encrypting and decrypting.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="encryptionAlgorithm" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="keyIv" /> is null.</exception>
        public EncryptionService(IEncryptionAlgorithm encryptionAlgorithm, EncryptionKeyIv keyIv)
        {
            _encryptionAlgorithm = encryptionAlgorithm ?? throw new ArgumentNullException(nameof(encryptionAlgorithm));
            _keyIv = keyIv ?? throw new ArgumentNullException(nameof(keyIv));
        }

        /// <summary>
        /// Encrypts the <paramref name="clearText" />.
        /// </summary>
        /// <param name="clearText">The clear text to encrypt.</param>
        /// <returns>The encrypted <paramref name="clearText" />.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="clearText" /> was null or empty.</exception>
        public byte[] Encrypt(string clearText)
        {
            if(string.IsNullOrEmpty(clearText))
                throw new ArgumentException("Clear text was null or empty");

            var clearTextBytes = System.Text.Encoding.Default.GetBytes(clearText);

            var transform = _encryptionAlgorithm.CreateEncryptor(_keyIv);

            return TransformBytes(transform, clearTextBytes);
        }

        /// <summary>
        /// Decrypts the <paramref name="cipher" />.
        /// </summary>
        /// <param name="cipher">The cipher to decrypt.</param>
        /// <returns>The decrypted <paramref name="cipher" />.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="cipher" /> was null or empty.</exception>
        public string Decrypt(byte[] cipher)
        {
            if(cipher == null || cipher.Length < 1)
                throw new ArgumentException("Cipher was null or empty.");

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
