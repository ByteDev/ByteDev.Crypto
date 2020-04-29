namespace ByteDev.Crypto.Hashing
{
    /// <summary>
    /// Represents the end encoding (from bytes) a hashed value
    /// should have.
    /// </summary>
    public enum HashEncoding
    {
        /// <summary>
        /// Base 64.
        /// </summary>
        Base64 = 0,

        /// <summary>
        /// Hexadecimal.
        /// </summary>
        Hex = 1
    }
}