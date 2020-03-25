using System;
using System.Linq;
using ByteDev.Collections;
using ByteDev.Crypto.Random;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Random
{
    [TestFixture]
    public class CryptoRandomTests
    {
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
        public class GenerateArray : CryptoRandomTests
        {
            [Test]
            public void WhenLengthIsZero_ThenReturnEmptyArray()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateArray(0);

                    Assert.That(result, Is.Empty);
                }
            }

            [Test]
            public void WhenOnlyOneValidChar_AndLengthOne_ThenReturnChar()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.GenerateArray(1);

                    Assert.That(result.Single(), Is.EqualTo('A'));
                }
            }

            [Test]
            public void WhenOnlyOneValidChar_ThenReturnSequenceOfChar()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.GenerateArray(3);

                    Assert.That(result.First(), Is.EqualTo('A'));
                    Assert.That(result.Second(), Is.EqualTo('A'));
                    Assert.That(result.Third(), Is.EqualTo('A'));
                }
            }

            [Test]
            public void WhenValidChars_ThenReturnOnlyValidChars()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateArray(100);

                    Assert.That(result.IsDigitsOnly, Is.True);
                }
            }

            [Test]
            public void WhenValidLength_ThenReturnCorrectLength()
            {
                const int length = 50;

                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateArray(length);

                    Assert.That(result.Length, Is.EqualTo(length));
                }
            }

            [Test]
            public void WhenLongEnoughLength_ThenUsesValidCharsEdgeCases()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateArray(1000);

                    Assert.That(result.Contains(CharacterSets.Digits[0]), Is.True);
                    Assert.That(result.Contains(CharacterSets.Digits[9]), Is.True);
                }
            }
        }

        [TestFixture]
        public class GenerateString_MinMaxLength : CryptoRandomTests
        {
            [Test]
            public void WhenMinIsZero_ThenThrowException()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() => sut.GenerateString(0, 1));
                }
            }

            [Test]
            public void WhenMinIsGreaterThanMax_ThenThrowException()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() => sut.GenerateString(2, 1));
                }
            }

            [Test]
            public void WhenMinIsMax_ThenReturnValidChars()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.GenerateString(3, 3);

                    Assert.That(result, Is.EqualTo("AAA"));
                }
            }

            [Test]
            public void WhenMinIsLessThanMax_ThenReturnRandomLength()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    for (var i = 0; i < 100; i++)
                    {
                        var result = sut.GenerateString(1, 2);

                        Assert.That(result, Is.EqualTo("A").Or.EqualTo("AA"));

                        Console.WriteLine(i + "=" + result);
                    }
                }
            }
        }

        [TestFixture]
        public class GenerateString : CryptoRandomTests
        {
            [Test]
            public void WhenLengthIsZero_ThenReturnEmptyString()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateString(0);

                    Assert.That(result, Is.Empty);
                }
            }

            [Test]
            public void WhenOnlyOneValidChar_AndLengthOne_ThenReturnChar()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.GenerateString(1);

                    Assert.That(result, Is.EqualTo("A"));
                }
            }

            [Test]
            public void WhenOnlyOneValidChar_ThenReturnSequenceOfChar()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.GenerateString(5);

                    Assert.That(result, Is.EqualTo("AAAAA"));
                }
            }

            [Test]
            public void WhenValidChars_ThenReturnOnlyValidChars()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateString(100);

                    Assert.That(result.IsDigitsOnly, Is.True);
                }
            }

            [Test]
            public void WhenValidLength_ThenReturnCorrectLength()
            {
                const int length = 50;

                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateString(length);

                    Assert.That(result.Length, Is.EqualTo(length));
                }
            }

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
    }
}