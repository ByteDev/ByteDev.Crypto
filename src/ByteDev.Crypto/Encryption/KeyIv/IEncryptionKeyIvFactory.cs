namespace ByteDev.Crypto.Encryption.KeyIv
{
    public interface IEncryptionKeyIvFactory
    {
        /// <summary>
        /// Get key and initialisation vector
        /// (Key: use same key for data in and out)
        /// (IV: random piece of data used in combination with the key.
        /// Each particular encrypted piece of text has its own VI)
        /// </summary>
        EncryptionKeyIv Create(string password, byte[] salt);
    }
}