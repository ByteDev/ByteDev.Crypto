namespace ByteDev.Crypto.Random
{
    /// <summary>
    /// Represents different sets of characters as strings.
    /// </summary>
    public static class CharacterSets
    {
        /// <summary>
        /// ASCII digits (0-9).
        /// </summary>
        public static readonly string Digits = "0123456789";

        /// <summary>
        /// ASCII upper case characters (A-Z).
        /// </summary>
        public static readonly string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// ASCII lower case characters (a-z).
        /// </summary>
        public static readonly string LowerCase = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Special printable ASCII characters (excluding space).
        /// </summary>
        public static readonly string AsciiSpecial = @"!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~";
    }
}