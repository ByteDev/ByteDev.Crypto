namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents an interface for performing hashing operations.
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// Hashes the given phrase.
        /// </summary>
        /// <param name="phrase">Phrase to hash.</param>
        /// <returns>Hash of <paramref name="phrase" /> as a string.</returns>
        string Hash(ClearPhrase phrase);

        /// <summary>
        /// Verify that the hash of <paramref name="phrase" /> is equal to <paramref name="expectedHash" />.
        /// </summary>
        /// <param name="phrase">Clear text phrase.</param>
        /// <param name="expectedHash">Hashed phrase.</param>
        /// <returns>True if phrase and hashed phrase are equal; otherwise returns false.</returns>
        bool Verify(ClearPhrase phrase, string expectedHash);

        /// <summary>
        /// Calculates a hash checksum for a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Hash checksum of the file as string.</returns>
        string CalcFileChecksum(string filePath);

        /// <summary>
        /// Calculates a hash checksum for a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="bufferSize">The number of bytes from the beginning of the file to create the checksum from.</param>
        /// <returns>Hash checksum of the file as string.</returns>
        string CalcFileChecksum(string filePath, int bufferSize);
    }
}