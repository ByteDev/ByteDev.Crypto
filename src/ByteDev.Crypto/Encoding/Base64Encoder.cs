using System;

namespace ByteDev.Crypto.Encoding
{
    internal static class Base64Encoder
    {
        public static string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] Decode(string base64)
        {
            return Convert.FromBase64String(base64);
        }
    }
}