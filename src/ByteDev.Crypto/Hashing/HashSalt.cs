using System;

namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents salt for when hashing a phrase.
    /// </summary>
    public class HashSalt
    {
        /// <summary>
        /// The salt bytes.
        /// </summary>
        public byte[] Bytes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashSalt" /> class.
        /// </summary>
        public HashSalt()
        {
            Bytes = new byte[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Crypto.Hashing.HashSalt" /> class.
        /// </summary>
        /// <param name="bytes">The salt bytes.</param>
        public HashSalt(byte[] bytes)
        {
            Bytes = bytes;
        }

        /// <summary>
        /// The salt encoded as a base64 string.
        /// </summary>
        /// <returns>Base64 string of the salt.</returns>
        public string ToBase64String()
        {
            return Convert.ToBase64String(Bytes);
        }

        /// <summary>
        /// The salt encoded as a hexadecimal string.
        /// </summary>
        /// <returns>Hexadecimal string of the salt.</returns>
        public string ToHexString()
        {
            return BitConverter.ToString(Bytes).Replace("-", "");
        }
    }
}