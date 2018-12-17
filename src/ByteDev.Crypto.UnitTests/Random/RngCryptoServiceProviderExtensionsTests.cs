using System.Security.Cryptography;
using ByteDev.Crypto.Random;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Random
{
    [TestFixture]
    public class RngCryptoServiceProviderExtensionsTests
    {
        [TestFixture]
        public class GetInt : RngCryptoServiceProviderExtensionsTests
        {
            [Test]
            public void WhenCalled_ThenReturnsRandomInt32()
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    var result1 = rng.GetInt();
                    var result2 = rng.GetInt();

                    Assert.That(result1, Is.Not.EqualTo(result2));
                }
            }
        }
    }
}