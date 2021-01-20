using ByteDev.Crypto.Encryption;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Encryption
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        [TestFixture]
        public class IsSensitive : ObjectExtensionsTests
        {
            [Test]
            public void WhenIsNull_ThenReturnFalse()
            {
                var result = (null as object).IsSensitive();

                Assert.That(result, Is.False);
            }

            [TestCase((long)10)]
            [TestCase((float)10.1)]
            [TestCase("10")]
            [TestCase("")]
            public void WhenIsNotCustomType_ThenReturnFalse(object sut)
            {
                var result = sut.IsSensitive();
                
                Assert.That(result, Is.False);
            }
            
            [Test]
            public void WhenHasNoEncryptProperties_ThenReturnsFalse()
            {
                var sut = TestNoAttributes.Create();

                var result = sut.IsSensitive();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenEncryptPropertiesAreNullOrEmpty_ThenReturnFalse()
            {
                var sut = TestHasAttributes.Create();

                sut.Name = null;
                sut.Address = string.Empty;

                var result = sut.IsSensitive();

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenAnyEncryptPropertiesSet_ThenReturnTrue()
            {
                var sut = TestHasAttributes.Create();

                sut.Name = null;

                var result = sut.IsSensitive();

                Assert.That(result, Is.True);
            }
        }
    }
}