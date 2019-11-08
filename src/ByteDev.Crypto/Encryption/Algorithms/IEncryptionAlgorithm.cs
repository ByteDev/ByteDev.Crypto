using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.KeyIv;

namespace ByteDev.Crypto.Encryption.Algorithms
{
    /// <summary>
    /// Represents an interface for encryption algorithms.
    /// </summary>
    public interface IEncryptionAlgorithm
    {
        /// <summary>
        /// The .NET algorithm type.
        /// </summary>
        SymmetricAlgorithm Algorithm { get; }

        /// <summary>
        /// Creates the encryptor object based on <paramref name="keyIv" />.
        /// </summary>
        /// <param name="keyIv">The encryption key IV.</param>
        /// <returns>Enryptor object.</returns>
        ICryptoTransform CreateEncryptor(EncryptionKeyIv keyIv);

        /// <summary>
        /// Creates the decryptor object based on <paramref name="keyIv" />.
        /// </summary>
        /// <param name="keyIv">The encryption key IV.</param>
        /// <returns>Decryptor object.</returns>
        ICryptoTransform CreateDecryptor(EncryptionKeyIv keyIv);
    }
}