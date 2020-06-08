using ByteDev.Base64;
using ByteDev.Crypto.Hashing;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class HashSaltTests
    {
        [TestFixture]
        public class Constructor : HashSaltTests
        {
            [Test]
            public void WhenCreatedWithNoBytes_ThenReturnEmpty()
            {
                var result = new HashSalt();

                Assert.That(result.Bytes, Is.Empty);
            }

            [Test]
            public void WhenCreatedWithBytes_ThenSetBytes()
            {
                byte[] bytes = { 1, 2, 3 };

                var result = new HashSalt(bytes);

                Assert.That(result.Bytes, Is.SameAs(bytes));
            }
        }

        [TestFixture]
        public class ToBase64String : HashSaltTests
        {
            [Test]
            public void WhenEmptyBytes_ThenReturnEmpty()
            {
                var sut = new HashSalt().ToBase64String();

                Assert.That(sut, Is.Empty);
            }

            [Test]
            public void WhenNotEmptyBytes_ThenReturnBase64String()
            {
                var result = Act(16).ToBase64String();

                Assert.That(result.IsBase64(), Is.True);
            }

            [Test]
            public void WhenStrengthIs32_ThenReturnBase64Length44()
            {
                var result = Act(32).ToBase64String();

                Assert.That(result.Length, Is.EqualTo(44));
            }
        }

        [TestFixture]
        public class ToHexString : HashSaltTests
        {
            [Test]
            public void WhenEmptyBytes_ThenReturnEmpty()
            {
                var sut = new HashSalt().ToHexString();

                Assert.That(sut, Is.Empty);
            }

            [Test]
            public void WhenStrengthIs32_ThenHexStringLength64()
            {
                var result = Act(32).ToHexString();
                
                Assert.That(result.Length, Is.EqualTo(64));
            }
        }

        private static HashSalt Act(int saltLength)
        {
            return new SaltFactory().Create(saltLength);
        }
    }
}