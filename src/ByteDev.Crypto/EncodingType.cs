namespace ByteDev.Crypto
{
    /// <summary>
    /// Represents the encoding type a hashed or encrypted value 
    /// should use when represented as a string.
    /// </summary>
    public enum EncodingType
    {
        /// <summary>
        /// Base 64.
        /// </summary>
        Base64 = 0,

        /// <summary>
        /// Hexadecimal.
        /// </summary>
        Hex = 1,

        /// <summary>
        /// Base 32.
        /// </summary>
        Base32 = 2
    }
}