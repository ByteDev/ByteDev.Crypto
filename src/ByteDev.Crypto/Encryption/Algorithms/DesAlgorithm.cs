﻿using System.Security.Cryptography;
using ByteDev.Crypto.Encryption.KeyIv;

namespace ByteDev.Crypto.Encryption.Algorithms
{
    /// <summary>
    /// Represents the encryption algorithm DES.
    /// </summary>
    public class DesAlgorithm : IEncryptionAlgorithm
    {
        /// <summary>
        /// The .NET algorithm type.
        /// </summary>
        public SymmetricAlgorithm Algorithm { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Encryption.Algorithms.DesAlgorithm" /> class.
        /// </summary>
        public DesAlgorithm()
        {
            Algorithm = new DESCryptoServiceProvider();
        }

        /// <summary>
        /// Creates the encryptor object based on <paramref name="keyIv" />.
        /// </summary>
        /// <param name="keyIv">The encryption key IV.</param>
        /// <returns>Enryptor object.</returns>
        public ICryptoTransform CreateEncryptor(EncryptionKeyIv keyIv)
        {
            return Algorithm.CreateEncryptor(keyIv.Key, keyIv.Iv);
        }

        /// <summary>
        /// Creates the decryptor object based on <paramref name="keyIv" />.
        /// </summary>
        /// <param name="keyIv">The encryption key IV.</param>
        /// <returns>Decryptor object.</returns>
        public ICryptoTransform CreateDecryptor(EncryptionKeyIv keyIv)
        {
            return Algorithm.CreateDecryptor(keyIv.Key, keyIv.Iv);
        }
    }
}