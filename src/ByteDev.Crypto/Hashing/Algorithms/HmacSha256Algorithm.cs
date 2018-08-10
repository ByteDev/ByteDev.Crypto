using System;
using System.Security.Cryptography;
using System.Text;

namespace ByteDev.Crypto.Hashing.Algorithms
{
    public class HmacSha256Algorithm : IHashAlgorithm
    {
        private readonly string _key;

        public HmacSha256Algorithm(string key)
        {
            _key = key;
        }

        public byte[] Hash(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length < 1)
                return new byte[0];

            using (var sha = new HMACSHA256(Encoding.UTF8.GetBytes(_key)))
            {
                return sha.ComputeHash(data);
            }
        }
    }
}