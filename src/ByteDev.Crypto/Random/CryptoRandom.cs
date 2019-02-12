using System;
using System.Security.Cryptography;
using System.Text;

namespace ByteDev.Crypto.Random
{
    public class CryptoRandom : IDisposable
    {
        private readonly string _characterSet;
        private readonly RNGCryptoServiceProvider _rng;

        public CryptoRandom(string characterSet)
        {
            if (string.IsNullOrEmpty(characterSet))
            {
                throw new ArgumentException("Character set was null or empty.", nameof(characterSet));
            }

            _characterSet = characterSet;
            _rng = new RNGCryptoServiceProvider();
        }

        public string GenerateString(int length)
        {
            var sb = new StringBuilder();

            while (sb.Length < length)
            {
                sb.Append(_characterSet[GetIndex()]);
            }
            
            return sb.ToString();
        }

        public char[] GenerateArray(int length)
        {
            var buffer = new char[length];

            for (var i = 0; i < length; i++)
            {
                buffer[i] = _characterSet[GetIndex()];
            }

            return buffer;
        }

        private int GetIndex()
        {
            var randomInt = _rng.GetInt();

            return randomInt % _characterSet.Length;
        }

        public void Dispose()
        {
            _rng?.Dispose();
        }
    }
}