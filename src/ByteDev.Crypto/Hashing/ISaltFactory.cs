namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents an interface for creating <see cref="T:ByteDev.Crypto.Hashing.HashSalt" /> objects.
    /// </summary>
    public interface ISaltFactory
    {
        /// <summary>
        /// Create a <see cref="T:ByteDev.Crypto.Hashing.HashSalt" />.
        /// </summary>
        /// <param name="saltLength">The length of the salt.</param>
        /// <returns>A new <see cref="T:ByteDev.Crypto.Hashing.HashSalt" />.</returns>
        HashSalt Create(int saltLength);
    }
}