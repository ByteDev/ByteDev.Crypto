using ByteDev.Crypto.Random;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Random
{
    [TestFixture]
    public class CryptoRandomTests
    {
        [TestFixture]
        public class GenerateString : CryptoRandomTests
        {
            [Test]
            public void WhenLongEnoughLength_ThenUsesValidCharsEdgeCases()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateString(1000);

                    StringAssert.Contains(CharacterSets.Digits[0].ToString(), result);
                    StringAssert.Contains(CharacterSets.Digits[9].ToString(), result);
                }
            }
        }

        [TestFixture]
        public class GenerateString_MinMaxLength : CryptoRandomTests
        {
            [Test]
            public void WhenMinIsLessThanMax_ThenReturnRandomLength()
            {
                var createdLen2 = false;
                var createdLen3 = false;

                using (var sut = new CryptoRandom("A"))
                {
                    for (var i = 0; i < 100; i++)
                    {
                        var result = sut.GenerateString(2, 3);

                        Assert.That(result, Is.EqualTo("AA").Or.EqualTo("AAA"));

                        if (result.Length == 2)
                            createdLen2 = true;

                        if (result.Length == 3)
                            createdLen3 = true;
                    }
                }

                Assert.That(createdLen2, Is.True);
                Assert.That(createdLen3, Is.True);
            }
            
            [Test]
            public void WhenLongEnoughLength_ThenUsesValidCharsEdgeCases()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateString(1000, 2000);

                    StringAssert.Contains(CharacterSets.Digits[0].ToString(), result);
                    StringAssert.Contains(CharacterSets.Digits[9].ToString(), result);
                }
            }
        }
    }
}