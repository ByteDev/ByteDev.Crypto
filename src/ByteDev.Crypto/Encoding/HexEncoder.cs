using System;
using System.Globalization;

namespace ByteDev.Crypto.Encoding
{
    internal static class HexEncoder
    {
        public static string Encode(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        public static byte[] Decode(string hex)
        {
            var bytes = new byte[hex.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
            }

            return bytes;
        }
    }
}