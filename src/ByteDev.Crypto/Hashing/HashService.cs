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
        public string Hash(HashPhrase phrase)
        {
            if(phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            var hash = CreateOneWayHash(phrase.Value);

            return Convert.ToBase64String(hash);
        }
        
        /// <summary>
        /// Verify that the hash of the given phrase is
        /// equal to the expected hashed phrase
        /// </summary>
        /// <param name="phrase">Clear text phrase</param>
        /// <param name="expectedHashedPhrase">Hashed base 64 phrase</param>
        /// <returns>Boolean value indicating if phrase and hashed phrase are equal</returns>
        public bool Verify(HashPhrase phrase, string expectedHashedPhrase)
        {
            if (phrase == null)
                throw new ArgumentNullException(nameof(phrase));

            if (expectedHashedPhrase == null)
                throw new ArgumentNullException(nameof(expectedHashedPhrase));

            var hash = Hash(phrase);

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
