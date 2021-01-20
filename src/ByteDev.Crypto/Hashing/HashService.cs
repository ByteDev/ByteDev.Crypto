using System;
using System.Text;
using ByteDev.Crypto.Hashing.Algorithms;
using ByteDev.Encoding;

namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents a service for performing hashing operations.
    /// </summary>
    public class HashService : IHashService
    {
        private readonly IEncoder _encoder;
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
        /// <exception cref="T:System.ArgumentNullException"><paramref name="hashAlgorithm" /> is null.</exception>
        public HashService(IHashAlgorithm hashAlgorithm) : this(hashAlgorithm, EncodingType.Base64)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashService" /> class.
        /// </summary>
        /// <param name="hashAlgorithm">Hashing algorithm to use when performing any hashing operation.</param>
        /// <param name="encodingType">Expected end string encoding of the hash.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="hashAlgorithm" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">Invalid EncodingType.</exception>
        public HashService(IHashAlgorithm hashAlgorithm, EncodingType encodingType)
        {
            _hashAlgorithm = hashAlgorithm ?? throw new ArgumentNullException(nameof(hashAlgorithm));
            _encoder = new EncoderFactory().Create(EncodingTypeConverter.ToEncodingLibType(encodingType));
        }

        /// <summary>
        /// Hashes the given phrase.
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
    }
}
