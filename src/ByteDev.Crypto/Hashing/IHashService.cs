namespace ByteDev.Crypto.Hashing
{
    public interface IHashService
    {
        /// <summary>
        /// Hashes the given phrase.
        /// </summary>
        /// <param name="phrase">Phrase to hash.</param>
        /// <returns>Hash of <paramref name="phrase" /> as a string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        string Hash(ClearPhrase phrase);

        /// <summary>
        /// Verify that the hash of <paramref name="phrase" /> is equal to <paramref name="expectedHash" />.
        /// </summary>
        /// <param name="phrase">Clear text phrase.</param>
        /// <param name="expectedHash">Hashed phrase.</param>
        /// <returns>True if phrase and hashed phrase are equal; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="expectedHash" /> is null.</exception>
        bool Verify(ClearPhrase phrase, string expectedHash);
    }
}