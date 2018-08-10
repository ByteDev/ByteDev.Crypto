using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.KeyIv;

namespace ByteDev.Crypto.Encryption.Algorithms
{
    public class TripleDesAlgorithm : IEncryptionAlgorithm
    {
        public SymmetricAlgorithm Algorithm { get; }

        public TripleDesAlgorithm()
        {
            Algorithm = new TripleDESCryptoServiceProvider();
        }

        public ICryptoTransform CreateEncryptor(EncryptionKeyIv keyIv)
        {
            return Algorithm.CreateEncryptor(keyIv.Key, keyIv.Iv);
        }

        public ICryptoTransform CreateDecryptor(EncryptionKeyIv keyIv)
        {
            return Algorithm.CreateDecryptor(keyIv.Key, keyIv.Iv);
        }
    }
}