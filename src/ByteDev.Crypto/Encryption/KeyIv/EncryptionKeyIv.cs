namespace ByteDev.Crypto.Encryption.KeyIv
{
    /// <summary>
    /// Represents a key and initialization vector.
    /// </summary>
    public class EncryptionKeyIv
    {
        /// <summary>
        /// Key.
        /// </summary>
        public byte[] Key { get; }

        /// <summary>
        /// Initialization vector.
        /// </summary>
        public byte[] Iv { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Encryption.KeyIv.EncryptionKeyIv" /> class.
        /// </summary>
        /// <param name="key">Key as byte array.</param>
        /// <param name="iv">Initialization vector as byte array.</param>
        public EncryptionKeyIv(byte[] key, byte[] iv)
        {
            Key = key;
            Iv = iv;
        }
    }
}