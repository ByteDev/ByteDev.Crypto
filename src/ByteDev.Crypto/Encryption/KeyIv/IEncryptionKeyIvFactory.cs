namespace ByteDev.Crypto.Encryption.KeyIv
{
    /// <summary>
    /// Represents an interface for creating <see cref="T:ByteDev.Crypto.Encryption.KeyIv.EncryptionKeyIv" />.
    /// </summary>
    public interface IEncryptionKeyIvFactory
    {
        /// <summary>
        /// Create key and initialization vector.
        /// </summary>
        /// <param name="password">Password to use when creating the <see cref="T:ByteDev.Crypto.Encryption.KeyIv.EncryptionKeyIv" />.</param>
        /// <param name="salt">Salt to use when creating the <see cref="T:ByteDev.Crypto.Encryption.KeyIv.EncryptionKeyIv" />.</param>
        /// <returns>Key and initialization vector object.</returns>
        EncryptionKeyIv Create(string password, byte[] salt);
    }
}