using ByteDev.Crypto.Hashing;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class SaltFactoryTests
    {
        private static SaltFactory CreateSut()
        {
            return new SaltFactory();
        }
    
        [Test]
        public void WhenStrengthIsZero_ThenCreateEmpty()
        {
            var result = Act(0);

            Assert.That(result.Bytes, Is.Empty);
        }

        [Test]
        public void WhenStrengthIsMinusOne_ThenCreateEmpty()
        {
            var result = Act(-1);

            Assert.That(result.Bytes, Is.Empty);
        }
        
        [Test]
        public void WhenCalledTwice_ThenReturnDifferentValues()
        {
            const int saltLength = 5;

            var result1 = Act(saltLength).ToBase64String();
            var result2 = Act(saltLength).ToBase64String();

            Assert.That(result1, Is.Not.EqualTo(result2));
        }

        private HashSalt Act(int saltLength)
        {
            var sut = CreateSut();

            return sut.Create(saltLength);
        }
    }
}