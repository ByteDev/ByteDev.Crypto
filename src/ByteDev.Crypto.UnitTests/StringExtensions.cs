namespace ByteDev.Crypto.UnitTests
{
    internal static class StringExtensions
    {
        public static byte[] GetBytes(this string text)
        {
            return System.Text.Encoding.Default.GetBytes(text);
        }
    }
}