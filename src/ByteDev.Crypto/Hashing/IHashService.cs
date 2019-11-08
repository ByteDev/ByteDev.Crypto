namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents an interface for hashing and verifying phrases.
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// One way hashes the given phrase.
        /// </summary>
        /// <param name="phrase">Phrase to hash.</param>
        /// <returns>Hash of <paramref name="phrase" /> as base64 string.</returns>
        string Hash(HashPhrase phrase);

        /// <summary>
        /// Verify that the hash of <paramref name="phrase" /> is equal to <paramref name="expectedHashedPhrase" />.
        /// </summary>
        /// <param name="phrase">Clear text phrase.</param>
        /// <param name="expectedHashedPhrase">Hashed base64 phrase.</param>
        /// <returns>True if phrase and hashed phrase are equal; otherwise returns false.</returns>
        bool Verify(HashPhrase phrase, string expectedHashedPhrase);
    }
}