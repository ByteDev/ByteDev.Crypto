using System.Text;

namespace ByteDev.Crypto.UnitTests
{
    internal static class StringExtensions
    {
        public static byte[] GetBytes(this string source)
        {
            return Encoding.Default.GetBytes(source);
        }

        public static bool IsDigitsOnly(this string source)
        {
            foreach (char c in source)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public static bool IsHex(this string source)
        {
            foreach(char c in source)
            {
                var isHex = c >= '0' && c <= '9' || 
                            c >= 'A' && c <= 'F' ||
                            c >= 'a' && c <= 'f';

                if(!isHex)
                    return false;
            }

            return true;
        }
    }
}