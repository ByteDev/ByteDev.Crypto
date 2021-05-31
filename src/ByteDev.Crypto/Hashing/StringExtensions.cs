namespace ByteDev.Crypto.Hashing
{
    internal static class StringExtensions
    {
        public static string FormatChecksum(this string source, EncodingType encodingType)
        {
            if (encodingType == EncodingType.Hex || encodingType == EncodingType.Base32)
                return source.ToUpper();

            return source;
        }
    }
}