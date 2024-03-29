﻿namespace ByteDev.Crypto.Random
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
        /// ASCII alpha-numeric characters (A-Za-z0-9).
        /// </summary>
        public static readonly string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Special printable ASCII characters (excluding white space).
        /// </summary>
        public static readonly string AsciiSpecial = @"!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~";

        /// <summary>
        /// Hexadecimal upper case characters (0-9A-F).
        /// </summary>
        public static readonly string UpperCaseHex = "0123456789ABCDEF";

        /// <summary>
        /// Hexadecimal lower case characters (0-9a-f).
        /// </summary>
        public static readonly string LowerCaseHex = "0123456789abcdef";
    }
}