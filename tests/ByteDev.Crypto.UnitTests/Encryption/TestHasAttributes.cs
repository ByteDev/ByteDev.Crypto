using ByteDev.Crypto.Encryption;

namespace ByteDev.Crypto.UnitTests.Encryption
{
    internal class TestHasAttributes
    {
        [Encrypt]
        public string Name { get; set; }

        [Encrypt]
        public string Address { get; set; }

        public string Country { get; set; }

        [Encrypt]
        public int Age { get; set; }
    }
}