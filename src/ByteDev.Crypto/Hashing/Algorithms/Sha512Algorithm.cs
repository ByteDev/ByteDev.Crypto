using System;
using System.Security.Cryptography;

namespace ByteDev.Crypto.Hashing.Algorithms
{
    /// <summary>
    /// Represents the hashing algorithm SHA512.
    /// </summary>
    public class Sha512Algorithm : IHashAlgorithm
    {
        /// <summary>
        /// Hash <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>The hash of <paramref name="data" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="data" /> is null.</exception>
        public byte[] Hash(byte[] data)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));

            if(data.Length < 1)
                return new byte[0];

            using (SHA512 sha512 = new SHA512Managed())
            {
                return sha512.ComputeHash(data);
            }
        }
    }
}