using System;
using System.Collections.Generic;
using System.IO;
using ByteDev.Crypto.Hashing.Algorithms;
using ByteDev.Encoding;

namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents a service for performing checksum operations on files.
    /// </summary>
    public class FileChecksumService : IFileChecksumService
    {
        private readonly IEncoder _encoder;
        private readonly IHashAlgorithm _hashAlgorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.FileChecksumService" /> class
        /// using the algorithm <see cref="T:ByteDev.Crypto.Hashing.Algorithms.Sha256Algorithm" />.
        /// </summary>
        public FileChecksumService() : this(new Sha256Algorithm())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.FileChecksumService" /> class.
        /// </summary>
        /// <param name="hashAlgorithm">Hashing algorithm to use when performing any hashing operation.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="hashAlgorithm" /> is null.</exception>
        public FileChecksumService(IHashAlgorithm hashAlgorithm) : this(hashAlgorithm, EncodingType.Base64)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.FileChecksumService" /> class.
        /// </summary>
        /// <param name="hashAlgorithm">Hashing algorithm to use when performing any hashing operation.</param>
        /// <param name="encodingType">Expected end string encoding of the hash.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="hashAlgorithm" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">Invalid EncodingType.</exception>
        public FileChecksumService(IHashAlgorithm hashAlgorithm, EncodingType encodingType)
        {
            _hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException(nameof(hashAlgorithm));
            _encoder = new EncoderFactory().Create(EncodingTypeConverter.ToEncodingLibType(encodingType));
        }

        /// <summary>
        /// Calculates a hash checksum for a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Hash checksum of the file as string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="filePath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="filePath" /> is empty.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">File is not found.</exception>
        public string Calculate(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = _hashAlgorithm.Hash(stream);

                return _encoder.Encode(hash);
            }
        }

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
        public string Calculate(string filePath, int bufferSize)
        {
            if (bufferSize < 1)
                throw new ArgumentOutOfRangeException(nameof(bufferSize), "Buffer size must be greater than zero.");

            var fileSize = new FileInfo(filePath).Length;

            if (bufferSize > fileSize)
                return Calculate(filePath);

            using (var stream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[bufferSize];

                stream.Read(buffer, 0, bufferSize);

                var hash = _hashAlgorithm.Hash(buffer);

                return _encoder.Encode(hash);
            }
        }

        /// <summary>
        /// Verify that the checksum of a file is equal to <paramref name="expectedChecksum" />.
        /// </summary>
        /// <param name="filePath">Path of file to check.</param>
        /// <param name="expectedChecksum">Expected checksum of the file.</param>
        /// <returns>True the file checksum is correct; otherwise false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="filePath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="filePath" /> is empty.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="expectedChecksum" /> is null.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">File is not found.</exception>
        public bool Verify(string filePath, string expectedChecksum)
        {
            if (expectedChecksum == null)
                throw new ArgumentNullException(nameof(expectedChecksum));

            var checkSum = Calculate(filePath);

            return expectedChecksum.Equals(checkSum, StringComparison.Ordinal);
        }

        /// <summary>
        /// Verify that the checksum of a file is equal to <paramref name="expectedChecksum" />.
        /// </summary>
        /// <param name="filePath">Path of file to check.</param>
        /// <param name="expectedChecksum">Expected checksum of the file.</param>
        /// <param name="bufferSize">The number of bytes from the beginning of the file to create the checksum from.</param>
        /// <returns>True the file checksum is correct; otherwise false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="filePath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="filePath" /> is empty.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="expectedChecksum" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="bufferSize" /> must be greater than zero.</exception>
        /// <exception cref="T:System.IO.FileNotFoundException">File is not found.</exception>
        public bool Verify(string filePath, string expectedChecksum, int bufferSize)
        {
            if (expectedChecksum == null)
                throw new ArgumentNullException(nameof(expectedChecksum));

            var checkSum = Calculate(filePath, bufferSize);

            return expectedChecksum.Equals(checkSum, StringComparison.Ordinal);
        }

        /// <summary>
        /// Searches the directory for files that have the expected checksum. Any matches
        /// will be returned as a list of paths.
        /// </summary>
        /// <param name="dirPath">Directory to check for matches.</param>
        /// <param name="expectedChecksum">Expected check sum.</param>
        /// <returns>List of file path of files that have the expected checksum.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="dirPath" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="dirPath" /> is empty.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">Directory not found.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="expectedChecksum" /> is null.</exception>
        public IList<string> Matches(string dirPath, string expectedChecksum)
        {
            if (expectedChecksum == null)
                throw new ArgumentNullException(nameof(expectedChecksum));

            var files = Directory.GetFiles(dirPath);

            var matches = new List<string>();

            foreach (var file in files)
            {
                if (Verify(file, expectedChecksum))
                    matches.Add(file);
            }

            return matches;
        }
    }
}