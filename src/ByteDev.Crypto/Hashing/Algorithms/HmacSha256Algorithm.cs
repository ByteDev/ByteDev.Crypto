using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ByteDev.Crypto.Hashing.Algorithms
{
    /// <summary>
    /// Represents the hashing algorithm HMACSHA256.
    /// </summary>
    public class HmacSha256Algorithm : IHashAlgorithm
    {
        private readonly string _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.Algorithms.HmacSha256Algorithm" /> class.
        /// </summary>
        /// <param name="key">Algorithm key to use when performing a hash operation.</param>
        public HmacSha256Algorithm(string key)
        {
            _key = key;
        }

        /// <summary>
        /// Hash <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>The hash of <paramref name="data" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="data" /> is null.</exception>
        public byte[] Hash(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Length < 1)
                return new byte[0];

            using (var sha = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(_key)))
            {
                return sha.ComputeHash(data);
            }
        }

        /// <summary>
        /// Hash <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The stream of data to hash</param>
        /// <returns>The hash of <paramref name="stream" />.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="stream" /> is null.</exception>
        public byte[] Hash(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            using (var sha = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(_key)))
            {
                return sha.ComputeHash(stream);
            }
        }
    }
}