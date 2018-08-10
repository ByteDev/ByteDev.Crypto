using System;
using System.Security.Cryptography;

namespace ByteDev.Crypto.Hashing.Algorithms
{
    public class Sha256Algorithm : IHashAlgorithm
    {
        public byte[] Hash(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length < 1)
                return new byte[0];

            using (SHA256 sha256 = new SHA256Managed())
            {
                return sha256.ComputeHash(data);
            }
        }
    }
}