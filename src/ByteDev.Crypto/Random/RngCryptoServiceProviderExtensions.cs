using System;
using System.Security.Cryptography;

namespace ByteDev.Crypto.Random
{
    internal static class RngCryptoServiceProviderExtensions
    {
        public static int GetInt32(this RNGCryptoServiceProvider source)
        {
            var buffer = new byte[sizeof(int)];
    
            source.GetBytes(buffer);

            return BitConverter.ToInt32(buffer, 0);
        }

        public static int GetInt32NonNegative(this RNGCryptoServiceProvider source)
        {
            var buffer = new byte[sizeof(int)];
    
            source.GetBytes(buffer);

            return BitConverter.ToInt32(buffer, 0) & int.MaxValue;
        }

        public static long GetInt64(this RNGCryptoServiceProvider source)
        {
            var buffer = new byte[sizeof(long)];
    
            source.GetBytes(buffer);

            return BitConverter.ToInt64(buffer, 0);
        }
    }
}