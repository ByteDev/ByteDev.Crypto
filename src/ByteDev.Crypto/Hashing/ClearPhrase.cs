using System;
using System.Text;

namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents a clear text phrase to hash.
    /// </summary>
    public class ClearPhrase
    {
        private System.Text.Encoding _encoding;

        /// <summary>
        /// Clear text phrase to hash.
        /// </summary>
        public string Phrase { get; }

        /// <summary>
        /// Any salt to apply to the phrase.
        /// </summary>
        public string Salt { get; }

        /// <summary>
        /// Any pepper to apply to the phrase.
        /// </summary>
        public string Pepper { get; }

        /// <summary>
        /// Encoding used for the phrase. Default is UTF-8.
        /// </summary>
        public System.Text.Encoding Encoding
        {
            get => _encoding ?? (_encoding = new UTF8Encoding());
            set => _encoding = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashPhrase" /> class.
        /// </summary>
        /// <param name="phrase">The phrase to hash.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        public ClearPhrase(string phrase) : this(phrase, string.Empty, string.Empty)
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashPhrase" /> class.
        /// </summary>
        /// <param name="phrase">The phrase to hash.</param>
        /// <param name="salt">Salt to apply when hashing the phrase.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="salt" /> is null.</exception>
        public ClearPhrase(string phrase, string salt) : this(phrase, salt, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashPhrase" /> class.
        /// </summary>
        /// <param name="phrase">The phrase to hash.</param>
        /// <param name="salt">Salt to apply when hashing the phrase.</param>
        /// <param name="pepper">Pepper to apply when hashing the phrase.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="phrase" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="salt" /> is null.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="pepper" /> is null.</exception>
        public ClearPhrase(string phrase, string salt, string pepper)
        {
            Phrase = phrase ?? throw new ArgumentNullException(nameof(phrase));
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
            Pepper = pepper ?? throw new ArgumentNullException(nameof(pepper));
        }

        /// <summary>
        /// Returns a string representation of <see cref="T:ByteDev.Crypto.Hashing.HashPhrase" />.
        /// (a concatenation of Phrase, Salt and Pepper).
        /// </summary>
        /// <returns>String representation of <see cref="T:ByteDev.Crypto.Hashing.HashPhrase" />.</returns>
        public override string ToString()
        {
            return Phrase + Salt + Pepper;
        }

        internal byte[] ToBytes()
        {
            return Encoding.GetBytes(ToString());
        }
    }
}