using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.KeyIv;

namespace ByteDev.Crypto.Encryption.Algorithms
{
    public interface IEncryptionAlgorithm
    {
        SymmetricAlgorithm Algorithm { get; }

        ICryptoTransform CreateEncryptor(EncryptionKeyIv keyIv);
        ICryptoTransform CreateDecryptor(EncryptionKeyIv keyIv);
    }
}