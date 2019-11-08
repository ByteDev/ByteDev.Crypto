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
    }
}