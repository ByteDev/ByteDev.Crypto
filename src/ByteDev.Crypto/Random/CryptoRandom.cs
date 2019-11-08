﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace ByteDev.Crypto.Random
{
    /// <summary>
    /// Represents a generator of cryptographically random strings and character arrays.
    /// </summary>
    public class CryptoRandom : IDisposable
    {
        private readonly string _characterSet;
        private readonly RNGCryptoServiceProvider _rng;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Random.CryptoRandom" /> class.
        /// </summary>
        /// <param name="characterSet">The character set to use when generating random strings or arrays of characters.</param>
        /// <exception cref="T:System.ArgumentException"><paramref name="characterSet" /> was null or empty.</exception>
        public CryptoRandom(string characterSet)
        {
            if (string.IsNullOrEmpty(characterSet))
                throw new ArgumentException("Character set was null or empty.", nameof(characterSet));

            _characterSet = characterSet;
            _rng = new RNGCryptoServiceProvider();
        }

        /// <summary>
        /// Generate a random string.
        /// </summary>
        /// <param name="length">The required length of the random string.</param>
        /// <returns>The random string.</returns>
        public string GenerateString(int length)
        {
            var sb = new StringBuilder();

            while (sb.Length < length)
            {
                sb.Append(_characterSet[GetIndex()]);
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// Generate a random an array of random characters.
        /// </summary>
        /// <param name="length">The required length of the random string.</param>
        /// <returns>The random string.</returns>
        public char[] GenerateArray(int length)
        {
            var buffer = new char[length];

            for (var i = 0; i < length; i++)
            {
                buffer[i] = _characterSet[GetIndex()];
            }

            return buffer;
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            _rng?.Dispose();
        }

        private int GetIndex()
        {
            var randomInt = _rng.GetInt();

            return randomInt % _characterSet.Length;
        }
    }
}