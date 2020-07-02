namespace ByteDev.Crypto.Shared.PackageTests
{
    internal static class StringExtensions
    {
        public static byte[] GetBytes(this string source)
        {
            return System.Text.Encoding.Default.GetBytes(source);
        }
    }
}