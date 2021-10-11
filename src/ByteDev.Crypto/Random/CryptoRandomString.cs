using System;
using System.Text;

namespace ByteDev.Crypto.Random
{
    /// <summary>
    /// Represents a generator of cryptographically random strings and character arrays.
    /// </summary>
    public class CryptoRandomString : CryptoRandomBase
    {
        private readonly string _characterSet;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Random.CryptoRandom" /> class.
        /// </summary>
        /// <param name="characterSet">The character set to use when generating random strings or arrays of characters.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="characterSet" /> was null or empty.</exception>
        public CryptoRandomString(string characterSet)
        {
            if (string.IsNullOrEmpty(characterSet))
                throw new ArgumentException("Character set was null or empty.", nameof(characterSet));

            _characterSet = characterSet;
        }

        /// <summary>
        /// Generates a random string of a certain length.
        /// </summary>
        /// <param name="length">The required length of the random string.</param>
        /// <returns>The random string.</returns>
        public string GenerateString(int length)
        {
            if (length < 1)
                return string.Empty;

            var sb = new StringBuilder();

            while (sb.Length < length)
            {
                sb.Append(_characterSet[GetIndex()]);
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// Generates a random string of random length between a given minimum and maximum.
        /// </summary>
        /// <param name="minLength">Minimum random length of the string.</param>
        /// <param name="maxLength">Maximum random length of the string.</param>
        /// <returns>The random string.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="minLength" /> was greater than <paramref name="maxLength" />.</exception>
        public string GenerateString(int minLength, int maxLength)
        {
            if (minLength < 0)
                minLength = 0;

            if (maxLength < 0)
                maxLength = 0;

            if (minLength > maxLength)
                throw new ArgumentOutOfRangeException(nameof(minLength), "Min length was greater than max length.");

            int length = GetRandomInt32(minLength, maxLength);

            return GenerateString(length);
        }

        /// <summary>
        /// Generate an array of random characters of a certain length.
        /// </summary>
        /// <param name="length">The required length of the random string.</param>
        /// <returns>The random string.</returns>
        public char[] GenerateArray(int length)
        {
            if (length < 1)
                return new char[0];

            var buffer = new char[length];

            for (var i = 0; i < length; i++)
            {
                buffer[i] = _characterSet[GetIndex()];
            }

            return buffer;
        }

        /// <summary>
        /// Generates an array of random characters of random length between a given minimum and maximum.
        /// </summary>
        /// <param name="minLength">Minimum random length of the array.</param>
        /// <param name="maxLength">Maximum random length of the array.</param>
        /// <returns>The random string.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="minLength" /> was greater than <paramref name="maxLength" />.</exception>
        public char[] GenerateArray(int minLength, int maxLength)
        {
            if (minLength < 0)
                minLength = 0;

            if (maxLength < 0)
                maxLength = 0;

            if (minLength > maxLength)
                throw new ArgumentOutOfRangeException(nameof(minLength), "Min length was greater than max length.");

            int length = GetRandomInt32(minLength, maxLength);

            return GenerateArray(length);
        }

        private int GetIndex()
        {
            var randomInt = _rng.GetInt32NonNegative();

            return randomInt % _characterSet.Length;
        }
    }
}