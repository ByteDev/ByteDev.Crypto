using System;
using ByteDev.Crypto.Random;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Random
{
    [TestFixture]
    public class CryptoRandomNumberTests : CryptoRandomStringTests
    {
        [TestFixture]
        public class GenerateInt32 : CryptoRandomNumberTests
        {
            [Test]
            public void WhenCalled_ThenReturnsInt()
            {
                using (var sut = new CryptoRandomNumber())
                {
                    var result1 = sut.GenerateInt32();
                    var result2 = sut.GenerateInt32();

                    Assert.That(result1, Is.Not.EqualTo(result2));
                }
            }
        }

        [TestFixture]
        public class GenerateInt32_MinMax : CryptoRandomNumberTests
        {
            [TestCase(-1)]
            [TestCase(0)]
            [TestCase(1)]
            public void WhenMinAndMaxEqual_ThenReturnSameInt(int value)
            {
                using (var sut = new CryptoRandomNumber())
                {
                    var result = sut.GenerateInt32(value, value);

                    Assert.That(result, Is.EqualTo(value));
                }
            }

            [Test]
            public void WhenMinIsGreaterThanMax_ThenThrowException()
            {
                using (var sut = new CryptoRandomNumber())
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() => _ = sut.GenerateInt32(2, 1));
                }
            }

            [Test]
            public void WhenMinIsNeg_AndMaxPositive_ThenReturnInt()
            {
                const int min = -5;
                const int max = 5;

                using (var sut = new CryptoRandomNumber())
                {
                    for (var i = 0; i < 100; i++)
                    {
                        var result = sut.GenerateInt32(min, max);

                        Assert.That(result, Is.GreaterThanOrEqualTo(min));
                        Assert.That(result, Is.LessThanOrEqualTo(max));
                    }
                }                
            }
        }
    }
}