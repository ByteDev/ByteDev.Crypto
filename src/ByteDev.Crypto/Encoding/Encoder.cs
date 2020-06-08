using System;

namespace ByteDev.Crypto.Encoding
{
    public class Encoder
    {
        private readonly EncodingType _encoding;

        public Encoder(EncodingType encoding)
        {
            _encoding = encoding;
        }

        public string Encode(byte[] bytes)
        {
            switch (_encoding)
            {
                case EncodingType.Base64:
                    return Base64Encoder.Encode(bytes);

                case EncodingType.Hex:
                    return HexEncoder.Encode(bytes);

                default:
                    throw new InvalidOperationException($"Unhandled {nameof(EncodingType)} value: '{_encoding}'.");
            }
        }

        public byte[] Decode(string text)
        {
            switch (_encoding)
            {
                case EncodingType.Base64:
                    return Base64Encoder.Decode(text);

                case EncodingType.Hex:
                    return HexEncoder.Decode(text);

                default:
                    throw new InvalidOperationException($"Unhandled {nameof(EncodingType)} value: '{_encoding}'.");
            }
        }
    }
}