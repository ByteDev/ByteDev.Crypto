using System;

namespace ByteDev.Crypto.Hashing
{
    public class HashSalt
    {
        public byte[] Bytes { get; }

        public HashSalt()
        {
            Bytes = new byte[0];
        }

        public HashSalt(byte[] bytes)
        {
            Bytes = bytes;
        }

        public string ToBase64String()
        {
            return Convert.ToBase64String(Bytes);
        }

        public string ToHexString()
        {
            return BitConverter.ToString(Bytes).Replace("-", "");
        }
    }
}