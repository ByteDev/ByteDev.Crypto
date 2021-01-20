using ByteDev.Crypto.Encryption;

namespace ByteDev.Crypto.UnitTests.Encryption
{
    internal class TestHasAttributes
    {
        [Encrypt]
        public string Name { get; set; }

        [Encrypt]
        public string Address { get; set; }

        // Will be ignored cos it is not string
        [Encrypt]
        public int Age { get; set; }

        // Will be ignored cos it does not have EncryptAttribute
        public string Country { get; set; }

        internal static TestHasAttributes Create()
        {
            return new TestHasAttributes
            {
                Name = "John", 
                Address = "Somewhere", 
                Age = 50, 
                Country = "UK"
            };
        }
    }
}