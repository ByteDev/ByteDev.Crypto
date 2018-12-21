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
            public void WhenOnlyOneValidChar_AndLengthOne_ThenReturnChar()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.CreateRandomString(1);

                    Assert.That(result, Is.EqualTo("A"));
                }
            }

            [Test]
            public void WhenOnlyOneValidChar_ThenReturnSequenceOfChar()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.CreateRandomString(5);

                    Assert.That(result, Is.EqualTo("AAAAA"));
                }
            }

            [Test]
            public void WhenValidChars_ThenReturnOnlyValidChars()
            {
                using (var sut = new CryptoRandom(Digits))
                {
                    var result = sut.CreateRandomString(100);

                    Assert.That(result.IsDigitsOnly, Is.True);
                }
            }

            [Test]
            public void WhenValidLength_ThenReturnCorrectLength()
            {
                const int length = 50;

                using (var sut = new CryptoRandom(Digits))
                {
                    var result = sut.CreateRandomString(length);

                    Assert.That(result.Length, Is.EqualTo(length));
                }
            }

            [Test]
            public void WhenLongEnoughLength_ThenUsesValidCharsEdgeCases()
            {
                using (var sut = new CryptoRandom(Digits))
                {
                    var result = sut.CreateRandomString(1000);

                    StringAssert.Contains(Digits[0].ToString(), result);
                    StringAssert.Contains(Digits[9].ToString(), result);
                }
            }
        }
    }
}