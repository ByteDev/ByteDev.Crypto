namespace ByteDev.Crypto.Hashing
{
    public interface IFileChecksumService
    {
        /// <summary>
        /// Calculates a hash checksum for a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Hash checksum of the file as string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="filePath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="filePath" /> is empty.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">File is not found.</exception>
        string Calculate(string filePath);

        /// <summary>
        /// Calculates a hash checksum for a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="bufferSize">The number of bytes from the beginning of the file to create the checksum from.</param>
        /// <returns>Hash checksum of the file as string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="filePath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="filePath" /> is empty.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="bufferSize" /> must be greater than zero.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">File is not found.</exception>
        string Calculate(string filePath, int bufferSize);

        /// <summary>
        /// Verify that the checksum of a file is equal to <paramref name="expectedCheckSum" />.
        /// </summary>
        /// <param name="filePath">Path of file to check.</param>
        /// <param name="expectedCheckSum">Expected checksum of the file.</param>
        /// <returns>True the file checksum is correct; otherwise false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="filePath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="filePath" /> is empty.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="expectedCheckSum" /> is null.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">File is not found.</exception>
        bool Verify(string filePath, string expectedCheckSum);

        /// <summary>
        /// Verify that the checksum of a file is equal to <paramref name="expectedCheckSum" />.
        /// </summary>
        /// <param name="filePath">Path of file to check.</param>
        /// <param name="expectedCheckSum">Expected checksum of the file.</param>
        /// <param name="bufferSize">The number of bytes from the beginning of the file to create the checksum from.</param>
        /// <returns>True the file checksum is correct; otherwise false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="filePath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="filePath" /> is empty.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="expectedCheckSum" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="bufferSize" /> must be greater than zero.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">File is not found.</exception>
        bool Verify(string filePath, string expectedCheckSum, int bufferSize);
    }
}