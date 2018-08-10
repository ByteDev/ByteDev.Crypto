using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.Algorithms;

namespace ByteDev.Crypto.Encryption.KeyIv
{
    public class EncryptionKeyIvFactory : IEncryptionKeyIvFactory
    {
        private readonly IEncryptionAlgorithm _algorithm;

        public EncryptionKeyIvFactory(IEncryptionAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        /// <summary>
        /// Get key and initialisation vector
        /// </summary>
        public EncryptionKeyIv Create(string password, byte[] salt)
        {
            var rfc = new Rfc2898DeriveBytes(password, salt);

            var key = rfc.GetBytes(_algorithm.Algorithm.KeySize / 8);
            var iv = rfc.GetBytes(_algorithm.Algorithm.BlockSize / 8);

            // (Key: use same key for data in and out)
            // (IV: random piece of data used in combination with the key.
            // Each particular encrypted piece of text has its own VI)

            return new EncryptionKeyIv(key, iv);
        } 
    }
}