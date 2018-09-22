using System;
using System.Security.Cryptography;

namespace ByteDev.Crypto.Hashing.Algorithms
{
    public class Sha1Algorithm : IHashAlgorithm
    {
        public byte[] Hash(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (data.Length < 1)
                return new byte[0];

            using (var sha1 = new SHA1Managed())
            {
                return sha1.ComputeHash(data);
            }
        }
    }
}