using System;

namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents a clear text phrase to hash.
    /// </summary>
    public class HashPhrase
    {
        /// <summary>
        /// Clear text phrase to hash
        /// </summary>
        public string Phrase { get; }

        /// <summary>
        /// Any salt to apply to the phrase
        /// </summary>
        public string Salt { get; }

        /// <summary>
        /// Any pepper to apply to the phrase
        /// </summary>
        public string Pepper { get; }
        
        public string Value => Phrase + Salt + Pepper;

        public HashPhrase(string phrase) : this(phrase, string.Empty, string.Empty)
        { 
        }

        public HashPhrase(string phrase, string salt) : this(phrase, salt, string.Empty)
        {
        }

        public HashPhrase(string phrase, string salt, string pepper)
        {
            Phrase = phrase ?? throw new ArgumentNullException(nameof(phrase));
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
            Pepper = pepper ?? throw new ArgumentNullException(nameof(pepper));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}