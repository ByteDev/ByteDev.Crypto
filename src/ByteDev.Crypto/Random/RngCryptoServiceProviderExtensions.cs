using System;
using System.Security.Cryptography;

namespace ByteDev.Crypto.Random
{
    internal static class RngCryptoServiceProviderExtensions
    {
        public static int GetInt(this RNGCryptoServiceProvider rng)
        {
            var intBuffer = new byte[4];

            rng.GetBytes(intBuffer);

            return BitConverter.ToInt32(intBuffer, 0) & int.MaxValue;
        }
    }
}