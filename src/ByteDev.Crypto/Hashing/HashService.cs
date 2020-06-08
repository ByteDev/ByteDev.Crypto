using System;
using System.IO;
using System.Text;
using ByteDev.Crypto.Encoding;
using ByteDev.Crypto.Hashing.Algorithms;
using Encoder = ByteDev.Crypto.Encoding.Encoder;

namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents a service for hashing and verifying phrases.
    /// </summary>
    public class HashService : IHashService
    {
        private readonly Encoder _encoder;
        private readonly IHashAlgorithm _hashAlgorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashService" /> class
        /// using the algorithm <see cref="T:ByteDev.Crypto.Hashing.Algorithms.Sha256Algorithm" />.
        /// </summary>
        public HashService() : this(new Sha256Algorithm())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashService" /> class.
        /// </summary>
        /// <param name="hashAlgorithm">Hashing algorithm to use when performing any hashing operation.</param>
        public HashService(IHashAlgorithm hashAlgorithm) : this(hashAlgorithm, EncodingType.Base64)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashService" /> class.
        /// </summary>
        /// <param name="hashAlgorithm">Hashing algorithm to use when performing any hashing operation.</param>
        /// <param name="encoding">Expected end string encoding of the hash.</param>
        public HashService(IHashAlgorithm hashAlgorithm, EncodingType encoding)
        {
            _hashAlgorithm = hashAlgorithm;
            _encoder = new Encoder(encoding);
        }

        /// <summary>
        /// One way hashes the given phrase.
        /// </summary>
        /// <param name="phrase">Phrase to hash.</param>
        /// <returns>Hash of <paramref name="phrase" /> as a string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        public string Hash(ClearPhrase phrase)
        {
            if(phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            var encoding = new UTF8Encoding();

            byte[] bytes = encoding.GetBytes(phrase.Value);

            var hash = _hashAlgorithm.Hash(bytes);

            return _encoder.Encode(hash);
        }

        /// <summary>
        /// Verify that the hash of <paramref name="phrase" /> is equal to <paramref name="expectedHash" />.
        /// </summary>
        /// <param name="phrase">Clear text phrase.</param>
        /// <param name="expectedHash">Hashed phrase.</param>
        /// <returns>True if phrase and hashed phrase are equal; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="expectedHash" /> is null.</exception>
        public bool Verify(ClearPhrase phrase, string expectedHash)
        {
            if (phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            if (expectedHash == null)
                throw new ArgumentNullException(nameof(expectedHash));

            var hash = Hash(phrase);

            return expectedHash.Equals(hash, StringComparison.Ordinal);
        }

        /// <summary>
        /// Calculates a hash checksum for a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Hash checksum of the file as string.</returns>
        public string CalcFileChecksum(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = _hashAlgorithm.Hash(stream);

                return _encoder.Encode(hash);
            }
        }
    }
}
