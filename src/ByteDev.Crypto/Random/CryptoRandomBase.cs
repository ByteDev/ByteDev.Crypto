using System;
using System.Security.Cryptography;

namespace ByteDev.Crypto.Random
{
    public abstract class CryptoRandomBase : IDisposable
    {
        protected readonly RNGCryptoServiceProvider _rng;

        protected CryptoRandomBase()
        {
            _rng = new RNGCryptoServiceProvider();
        }

        public void Dispose()
        {
            _rng?.Dispose();
        }

        protected internal int GetRandomInt32(int min, int max)
        {
            if (min > max)
                throw new ArgumentOutOfRangeException(nameof(min), "Min was greater than max.");

            if (min == max) 
                return min;

            int generatedValue = Math.Abs(_rng.GetInt32());

            int diff = max - min + 1;
            int mod = generatedValue % diff;
            
            return min + mod;
        }
    }
}