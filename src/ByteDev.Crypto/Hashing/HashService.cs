using System;
using System.Text;
using ByteDev.Crypto.Hashing.Algorithms;

namespace ByteDev.Crypto.Hashing
{
    public class HashService : IHashService
    {
        private readonly IHashAlgorithm _hashAlgorithm;

        public HashService() : this(new Sha256Algorithm())
        {
        }

        public HashService(IHashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        /// <summary>
        /// One way hashes the given phrase
        /// </summary>
        /// <param name="phrase">Phrase to hash</param>
        /// <returns>Hash of phrase as base64 string</returns>
        public string Hash(string phrase)
        {
            return Hash(phrase, string.Empty);
        }

        /// <summary>
        /// One way hashes the given phrase with the given salt
        /// </summary>
        /// <param name="phrase">Phrase to hash</param>
        /// <param name="salt">Salt to add to phrase</param>
        /// <returns>Hash of salted phrase as base64 string</returns>
        public string Hash(string phrase, string salt)
        {
            if(phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            var hash = CreateOneWayHash(phrase + salt);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Verify that the hash of the given phrase are
        /// equal to the given hashed phrase
        /// </summary>
        /// <param name="phrase">Clear text phrase</param>
        /// <param name="expectedHashedPhrase">Hashed base 64 phrase</param>
        /// <returns>Boolean value indicating if phrase and hashed phrase are equal</returns>
        public bool Verify(string phrase, string expectedHashedPhrase)
        {
            return Verify(phrase, string.Empty, expectedHashedPhrase);
        }

        /// <summary>
        /// Verify that the hash of the given phrase plus salt are
        /// equal to the given hashed phrase
        /// </summary>
        /// <param name="phrase">Clear text phrase</param>
        /// <param name="salt">Salt for phrase</param>
        /// <param name="expectedHashedPhrase">Hashed base 64 phrase</param>
        /// <returns>Boolean value indicating if phrase and hashed phrase are equal</returns>
        public bool Verify(string phrase, string salt, string expectedHashedPhrase)
        {
            if (phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            if(expectedHashedPhrase == null)
                throw new ArgumentNullException(nameof(expectedHashedPhrase));

            var hash = Hash(phrase, salt);

            return expectedHashedPhrase.Equals(hash, StringComparison.Ordinal);
        }

        private byte[] CreateOneWayHash(string phrase)
        {
            var encoding = new UTF8Encoding();

            byte[] bytes = encoding.GetBytes(phrase);
            return _hashAlgorithm.Hash(bytes);
        }
    }
}
