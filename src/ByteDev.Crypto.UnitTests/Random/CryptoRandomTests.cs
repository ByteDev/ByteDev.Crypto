using System;
using ByteDev.Crypto.Random;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Random
{
    [TestFixture]
    public class CryptoRandomTests
    {
        private const string Digits = "1234567890";

        [TestFixture]
        public class Constructor : CryptoRandomTests
        {
            [Test]
            public void WhenValidCharsStringIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => new CryptoRandom(null));
            }

            [Test]
            public void WhenValidCharsStringIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => new CryptoRandom(string.Empty));
            }
        }

        [TestFixture]
        public class CreateRandomString : CryptoRandomTests
        {
            [Test]
            public void WhenLengthIsZero_ThenReturnEmptyString()
            {
                using (var sut = new CryptoRandom(Digits))
                {
                    var result = sut.CreateRandomString(0);

                    Assert.That(result, Is.Empty);
                }
            }

            [Test]
            public void WhenValidLength_ThenReturnRandomString()
            {
                using (var sut = new CryptoRandom(Digits))
                {
                    var result = sut.CreateRandomString(10);

                    Assert.That(result.Length, Is.EqualTo(10));
                    Assert.That(result.IsDigitsOnly, Is.True);
                }
            }
        }
    }
}