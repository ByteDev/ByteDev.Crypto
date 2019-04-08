using System.Security.Cryptography;

namespace ByteDev.Crypto.Hashing
{
    public class SaltFactory : ISaltFactory
    {
        public HashSalt Create(int saltLength)
        {
            if (saltLength <= 0)
            {
                return new HashSalt();
            }

            using (var provider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[saltLength];

                provider.GetBytes(bytes);

                return new HashSalt(bytes);
            }
        }
    }
}