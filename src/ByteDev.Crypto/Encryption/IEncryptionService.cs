namespace ByteDev.Crypto.Encryption
{
    /// <summary>
    /// Represents an interface for a service for encrypting and decrypting. 
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypts the <paramref name="clearText" />.
        /// </summary>
        /// <param name="clearText">The clear text to encrypt.</param>
        /// <returns>The encrypted <paramref name="clearText" />.</returns>
        byte[] Encrypt(string clearText);

        /// <summary>
        /// Decrypts the <paramref name="cipher" />.
        /// </summary>
        /// <param name="cipher">The cipher to decrypt.</param>
        /// <returns>The decrypted <paramref name="cipher" />.</returns>
        string Decrypt(byte[] cipher);
    }
}