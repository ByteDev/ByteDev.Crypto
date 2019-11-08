using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.Algorithms;

namespace ByteDev.Crypto.Encryption.KeyIv
{
    /// <summary>
    /// Represents a factory for creating <see cref="T:ByteDev.Crypto.Encryption.KeyIv.EncryptionKeyIv" />.
    /// </summary>
    public class EncryptionKeyIvFactory : IEncryptionKeyIvFactory
    {
        private readonly IEncryptionAlgorithm _algorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Encryption.KeyIv.EncryptionKeyIvFactory" /> class.
        /// </summary>
        /// <param name="algorithm">Algorithm to use when encrypting.</param>
        public EncryptionKeyIvFactory(IEncryptionAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        /// <summary>
        /// Create key and initialization vector.
        /// </summary>
        /// <param name="password">Password to use when creating the <see cref="T:ByteDev.Crypto.Encryption.KeyIv.EncryptionKeyIv" />.</param>
        /// <param name="salt">Salt to use when creating the <see cref="T:ByteDev.Crypto.Encryption.KeyIv.EncryptionKeyIv" />.</param>
        /// <returns>Key and initialization vector object.</returns>
        public EncryptionKeyIv Create(string password, byte[] salt)
        {
            var rfc = new Rfc2898DeriveBytes(password, salt);

            var key = rfc.GetBytes(_algorithm.Algorithm.KeySize / 8);
            var iv = rfc.GetBytes(_algorithm.Algorithm.BlockSize / 8);

            // Key: use same key for data in and out.
            // IV: random piece of data used in combination with the key.
            // Each particular encrypted piece of text has its own VI.

            return new EncryptionKeyIv(key, iv);
        } 
    }
}