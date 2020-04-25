namespace ByteDev.Crypto.UnitTests
{
    internal static class CharArrayExtensions
    {
        public static bool IsDigitsOnly(this char[] source)
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