using System.Text;

namespace ByteDev.Crypto.UnitTests
{
    internal static class StringExtensions
    {
        public static byte[] GetBytes(this string text)
        {
            return Encoding.Default.GetBytes(text);
        }
    }
}