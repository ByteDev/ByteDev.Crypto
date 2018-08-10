using System;
using System.Security.Cryptography;

namespace ByteDev.Crypto.Hashing
{
    public class SaltFactory : ISaltFactory
    {
        public byte[] Create(int saltLength)
        {
            if (saltLength <= 0)
            {
                return new byte[0];
            }

            using (var provider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[saltLength];

                provider.GetBytes(bytes);

                return bytes;
            }
        }

        public string CreateAsBase64(int saltLength)
        {
            var bytes = Create(saltLength);

            return Convert.ToBase64String(bytes);
        }

        public string CreateAsHex(int saltLength)
        {
            var bytes = Create(saltLength);

            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}