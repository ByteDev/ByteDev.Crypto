using System;
using System.IO;
using System.Text;
using ByteDev.Crypto.Hashing.Algorithms;

namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents a service for hashing and verifying phrases.
    /// </summary>
    public class HashService : IHashService
    {
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
        public HashService(IHashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }
        
        /// <summary>
        /// One way hashes the given phrase and returns it base64 encoded.
        /// </summary>
        /// <param name="phrase">Phrase to hash.</param>
        /// <returns>Hash of <paramref name="phrase" /> as base64 string.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        public string Hash(HashPhrase phrase)
        {
            if(phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            var hash = CreateOneWayHash(phrase.Value);

            return Convert.ToBase64String(hash);
        }
        
        /// <summary>
        /// Verify that the hash of <paramref name="phrase" /> is equal to <paramref name="expectedHashedPhrase" />.
        /// </summary>
        /// <param name="phrase">Clear text phrase.</param>
        /// <param name="expectedHashedPhrase">Hashed base64 phrase.</param>
        /// <returns>True if phrase and hashed phrase are equal; otherwise returns false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="expectedHashedPhrase" /> is null.</exception>
        public bool Verify(HashPhrase phrase, string expectedHashedPhrase)
        {
            if (phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            if (expectedHashedPhrase == null)
                throw new ArgumentNullException(nameof(expectedHashedPhrase));

            var hash = Hash(phrase);

            return expectedHashedPhrase.Equals(hash, StringComparison.Ordinal);
        }

        /// <summary>
        /// Calculates a hash checksum for a file.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Hash checksum of the file as base64 string.</returns>
        public string CalcFileChecksum(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var hash = _hashAlgorithm.Hash(stream);

                return Convert.ToBase64String(hash);
            }
        }

        private byte[] CreateOneWayHash(string phrase)
        {
            var encoding = new UTF8Encoding();

            byte[] bytes = encoding.GetBytes(phrase);

            return _hashAlgorithm.Hash(bytes);
        }
    }
}
