using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.KeyIv;

namespace ByteDev.Crypto.Encryption.Algorithms
{
    public class Rc2Algorithm : IEncryptionAlgorithm
    {
        public SymmetricAlgorithm Algorithm { get; }

        public Rc2Algorithm()
        {
            Algorithm = new RC2CryptoServiceProvider();
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