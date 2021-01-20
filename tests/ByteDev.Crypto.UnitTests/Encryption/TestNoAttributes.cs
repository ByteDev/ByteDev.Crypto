namespace ByteDev.Crypto.UnitTests.Encryption
{
    internal class TestNoAttributes
    {
        public string Name { get; set; }

        public int Age { get; set; }

        internal static TestNoAttributes Create()
        {
            return new TestNoAttributes
            {
                Age = 50,
                Name = "John"
            };
        }
    }
}