using System;
using System.IO;
using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.Algorithms;
using ByteDev.Crypto.Encryption.KeyIv;
using ByteDev.Encoding;

namespace ByteDev.Crypto.Encryption
{
    /// <summary>
    /// Represents a service for encrypting and decrypting. 
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        private readonly IEncryptionAlgorithm _encryptionAlgorithm;
        private readonly EncryptionKeyIv _keyIv;
        private readonly IEncoderFactory _encoderFactory;

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
            _encoderFactory = new EncoderFactory();
        }

        /// <summary>
        /// Encrypts <paramref name="clearText" />.
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
        /// Decrypts <paramref name="cipher" />.
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

        /// <summary>
        /// Encrypts all the public property strings with a <see cref="T:ByteDev.Crypto.Encryption.EncryptAttribute" />.
        /// </summary>
        /// <param name="obj">Object to encrypt.</param>
        /// <param name="encoding">Encoding to use after encrypting.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> is null.</exception>
        public void EncryptProperties(object obj, EncodingType encoding = EncodingType.Base64)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var encoder = _encoderFactory.Create(EncodingTypeConverter.ToEncodingLibType(encoding));

            var properties = obj.GetType().GetPropertiesWithAttribute<EncryptAttribute>();

            foreach (var property in properties)
            {
                if (property.PropertyType != typeof(string))
                    continue;

                byte[] cipher = Encrypt(property.GetValue(obj, null).ToString());

                var text = encoder.Encode(cipher);

                property.SetValue(obj, text, null);
            }
        }

        /// <summary>
        /// Decrypt all the public property strings with a <see cref="T:ByteDev.Crypto.Encryption.EncryptAttribute" />.
        /// </summary>
        /// <param name="obj">Object to decrypt.</param>
        /// <param name="encoding">Encoding of the property strings.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="obj" /> is null.</exception>
        public void DecryptProperties(object obj, EncodingType encoding = EncodingType.Base64)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var encoder = _encoderFactory.Create(EncodingTypeConverter.ToEncodingLibType(encoding));

            var properties = obj.GetType().GetPropertiesWithAttribute<EncryptAttribute>();

            foreach (var property in properties)
            {
                if (property.PropertyType != typeof(string))
                    continue;

                var text = property.GetValue(obj, null).ToString();

                byte[] cipher = encoder.DecodeToBytes(text);

                var clearText = Decrypt(cipher);

                property.SetValue(obj, clearText, null);
            }
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
