﻿namespace ByteDev.Crypto.Random
{
    /// <summary>
    /// Represents different sets of characters as strings.
    /// </summary>
    public static class CharacterSets
    {
        /// <summary>
        /// Characters: 0123456789
        /// </summary>
        public static readonly string Digits = "0123456789";

        /// <summary>
        /// Characters: ABCDEFGHIJKLMNOPQRSTUVWXYZ
        /// </summary>
        public static readonly string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Characters: abcdefghijklmnopqrstuvwxyz
        /// </summary>
        public static readonly string LowerCase = "abcdefghijklmnopqrstuvwxyz";
    }
}