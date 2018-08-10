using ByteDev.Common.Encoding;
using ByteDev.Crypto.Hashing;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class SaltFactoryTest
    {
        public SaltFactory CreateSut()
        {
            return new SaltFactory();
        }

        [TestFixture]
        public class CreateAsBase64 : SaltFactoryTest
        {
            [Test]
            public void WhenStrengthIsZero_ThenCreateEmpty()
            {
                var result = Act(0);

                Assert.That(result, Is.EqualTo(string.Empty));
            }

            [Test]
            public void WhenStrengthIsMinusOne_ThenCreateEmpty()
            {
                var result = Act(-1);

                Assert.That(result, Is.EqualTo(string.Empty));
            }

            [Test]
            public void WhenStrengthIsGreaterThanZero_ThenCreateBase64Salt()
            {
                var result = Act(16);

                Assert.That(Base64.IsBase64Encoded(result), Is.True);
            }

            [Test]
            public void WhenStrengthIs32_ThenCreateBase64SaltOfLength44()
            {
                var result = Act(32);

                Assert.That(result.Length, Is.EqualTo(44));
            }

            [Test]
            public void WhenCalledTwice_ThenReturnDifferentStrings()
            {
                const int saltLength = 5;

                var result1 = Act(saltLength);
                var result2 = Act(saltLength);

                Assert.That(result1, Is.Not.EqualTo(result2));
            }

            private string Act(int saltLength)
            {
                var sut = CreateSut();

                return sut.CreateAsBase64(saltLength);
            }
        }

        [TestFixture]
        public class CreateAsHex : SaltFactoryTest
        {
            [Test]
            public void WhenStrengthIs32_ThenCreateHexSaltOfLength44()
            {
                var result = Act(32);

                Assert.That(result.Length, Is.EqualTo(64));
            }

            private string Act(int saltLength)
            {
                var sut = CreateSut();

                return sut.CreateAsHex(saltLength);
            }
        }
    }
}