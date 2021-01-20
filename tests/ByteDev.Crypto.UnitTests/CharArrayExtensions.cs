namespace ByteDev.Crypto.UnitTests
{
    internal static class CharArrayExtensions
    {
        public static bool IsDigitsOnly(this char[] source)
        {
            foreach (char c in source)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }
    }
}