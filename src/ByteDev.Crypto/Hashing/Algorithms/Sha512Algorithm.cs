using System;
using System.Security.Cryptography;

namespace ByteDev.Crypto.Hashing.Algorithms
{
    public class Sha512Algorithm : IHashAlgorithm
    {
        public byte[] Hash(byte[] data)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));
            if(data.Length < 1)
                return new byte[0];

            using (SHA512 sha512 = new SHA512Managed())
            {
                return sha512.ComputeHash(data);
            }
        }
    }
}