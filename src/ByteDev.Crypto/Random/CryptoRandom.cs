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
                var index = GetIndex(_validChars.Length);
                sb.Append(_validChars[index]);
            }

            return sb.ToString();
        }

        private int GetIndex(int maxIndex)
        {
            int value;

            do
            {
                value = _rng.GetInt();
            } while (value >= maxIndex * (int.MaxValue / maxIndex));

            return value % maxIndex;
        }

        public void Dispose()
        {
            _rng?.Dispose();
        }
    }
}