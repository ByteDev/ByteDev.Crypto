using System.IO;

namespace ByteDev.Crypto.Hashing.Algorithms
{
    /// <summary>
    /// Represents an interface for hashing algorithms.
    /// </summary>
    public interface IHashAlgorithm
    {
        /// <summary>
        /// Hash <paramref name="data" />.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>The hash of <paramref name="data" />.</returns>
        byte[] Hash(byte[] data);

        /// <summary>
        /// Hash <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">The stream of data to hash</param>
        /// <returns>The hash of <paramref name="stream" />.</returns>
        byte[] Hash(Stream stream);
    }
}