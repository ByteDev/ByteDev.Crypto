using System;
using System.Linq;
using ByteDev.Collections;
using ByteDev.Crypto.Random;
using ByteDev.Strings;
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
                Assert.Throws<ArgumentException>(() => _ = new CryptoRandom(null));
            }

            [Test]
            public void WhenValidCharsStringIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _ = new CryptoRandom(string.Empty));
            }
        }

        [TestFixture]
        public class GenerateArray : CryptoRandomTests
        {
            [TestCase(-1)]
            [TestCase(0)]
            public void WhenLengthIsLessThanOne_ThenReturnEmptyArray(int length)
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateArray(length);

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

                    Assert.That(result.Contains(CharacterSets.Digits.First()), Is.True);
                    Assert.That(result.Contains(CharacterSets.Digits.Last()), Is.True);
                }
            }
        }

        [TestFixture]
        public class GenerateArray_MinMaxLength : CryptoRandomTests
        {
            [Test]
            public void WhenMinIsGreaterThanMax_ThenThrowException()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() => sut.GenerateArray(2, 1));
                }
            }

            [Test]
            public void WhenMinIsMinusAndMaxIsZero_ThenReturnEmpty()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                     var result = sut.GenerateArray(-1, 0);

                     Assert.That(result, Is.Empty);
                }
            }

            [Test]
            public void WhenMinIsLessThanMax_ThenReturnRandomLength()
            {
                var createdLen2 = false;
                var createdLen3 = false;

                using (var sut = new CryptoRandom("A"))
                {
                    for (var i = 0; i < 100; i++)
                    {
                        var result = sut.GenerateArray(2, 3);

                        if (result.Length == 2)
                        {
                            createdLen2 = true;
                            Assert.That(result.First(), Is.EqualTo('A'));
                            Assert.That(result.Second(), Is.EqualTo('A'));
                        }
                        else if (result.Length == 3)
                        {
                            createdLen3 = true;
                            Assert.That(result.First(), Is.EqualTo('A'));
                            Assert.That(result.Second(), Is.EqualTo('A'));
                            Assert.That(result.Third(), Is.EqualTo('A'));
                        }
                        else
                        {
                            Assert.Fail($"Array was length {result.Length}.");
                        }
                    }
                }

                Assert.That(createdLen2, Is.True);
                Assert.That(createdLen3, Is.True);
            }
        }

        [TestFixture]
        public class GenerateString : CryptoRandomTests
        {
            [TestCase(-1)]
            [TestCase(0)]
            public void WhenLengthIsLessThanOne_ThenReturnEmpty(int length)
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateString(length);

                    Assert.That(result, Is.Empty);
                }
            }

            [Test]
            public void WhenOnlyOneValidChar_AndLengthOne_ThenReturnOneCharString()
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
                    var result = sut.GenerateString(3);

                    Assert.That(result, Is.EqualTo("AAA"));
                }
            }

            [Test]
            public void WhenValidChars_ThenReturnOnlyValidChars()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    var result = sut.GenerateString(100);

                    Assert.That(result.IsDigits(), Is.True);
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

                    StringAssert.Contains(CharacterSets.Digits.First().ToString(), result);
                    StringAssert.Contains(CharacterSets.Digits.Last().ToString(), result);
                }
            }
        }

        [TestFixture]
        public class GenerateString_MinMaxLength : CryptoRandomTests
        {
            [Test]
            public void WhenMinIsGreaterThanMax_ThenThrowException()
            {
                using (var sut = new CryptoRandom(CharacterSets.Digits))
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() => sut.GenerateString(2, 1));
                }
            }
            
            [TestCase(-1)]
            [TestCase(0)]
            public void WhenMinIsLessThanOneAndMaxIsZero_ThenReturnEmpty(int minLength)
            {
                using (var sut = new CryptoRandom("A"))
                {
                    for (var i = 0; i < 100; i++)
                    {
                        var result = sut.GenerateString(minLength, 0);

                        Assert.That(result, Is.Empty);
                    }
                }
            }
            
            [Test]
            public void WhenMinAndMaxIsOne_ThenReturnOneLengthString()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.GenerateString(1, 1);

                    Assert.That(result, Is.EqualTo("A"));
                }
            }

            [Test]
            public void WhenMinAndMaxIsEqual_ThenReturnValidString()
            {
                using (var sut = new CryptoRandom("A"))
                {
                    var result = sut.GenerateString(2, 2);

                    Assert.That(result, Is.EqualTo("AA"));
                }
            }

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

                    StringAssert.Contains(CharacterSets.Digits.First().ToString(), result);
                    StringAssert.Contains(CharacterSets.Digits.Last().ToString(), result);
                }
            }
        }
    }
}