using System.Security.Cryptography;

namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents a factory for creating <see cref="T:ByteDev.Crypto.Hashing.HashSalt" /> objects.
    /// </summary>
    public class SaltFactory : ISaltFactory
    {
        /// <summary>
        /// Create a <see cref="T:ByteDev.Crypto.Hashing.HashSalt" />.
        /// </summary>
        /// <param name="saltLength">The length of the salt.</param>
        /// <returns>A new <see cref="T:ByteDev.Crypto.Hashing.HashSalt" />.</returns>
        public HashSalt Create(int saltLength)
        {
            if (saltLength <= 0)
                return new HashSalt();

            using (var provider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[saltLength];

                provider.GetBytes(bytes);

                return new HashSalt(bytes);
            }
        }
    }
}