using System;
using System.Security.Cryptography;
using System.Text;

namespace ByteDev.Crypto.Random
{
    public class CryptoRandom : IDisposable
    {
        private readonly string _validChars;
        private readonly RNGCryptoServiceProvider _rng;

        public CryptoRandom(string validChars)
        {
            if (string.IsNullOrEmpty(validChars))
            {
                throw new ArgumentException("Valid character string was null or empty.", nameof(validChars));
            }

            _validChars = validChars;
            _rng = new RNGCryptoServiceProvider();
        }

        public string CreateRandomString(int length)
        {
            var sb = new StringBuilder();

            while (sb.Length < length)
            {
                sb.Append(_validChars[GetIndex()]);
            }

            return sb.ToString();
        }

        private int GetIndex()
        {
            var randomInt = _rng.GetInt();

            return randomInt % _validChars.Length;
        }

        public void Dispose()
        {
            _rng?.Dispose();
        }
    }
}