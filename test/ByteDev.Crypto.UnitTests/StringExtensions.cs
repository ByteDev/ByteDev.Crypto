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
    }
}