using System;

namespace ByteDev.Crypto
{
    internal static class EncodingTypeConverter
    {
        public static Encoding.EncodingType ToEncodingLibType(EncodingType encodingType)
        {
            switch (encodingType)
            {
                case EncodingType.Base64:
                    return Encoding.EncodingType.Base64;
                case EncodingType.Hex:
                    return Encoding.EncodingType.Hex;
                case EncodingType.Base32:
                    return Encoding.EncodingType.Base32;
                default:
                    throw new InvalidOperationException($"Unhandled Crypto encoding type: {encodingType}.");
            }
        }
    }
}