using System;
using System.Linq;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests
{
    [TestFixture]
    public class CharacterPositionCheckerTests
    {
        private const string PhraseString = "John";

        private static readonly char[] PhraseArray = { 'J', 'o', 'h', 'n' };

        [TestFixture]
        public class VerifyString : CharacterPositionCheckerTests
        {
            [Test]
            public void WhenPhraseIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CharacterPositionChecker.Verify(null as string, new[] { new CharacterPosition('J', 0) }));
            }

            [Test]
            public void WhenPhraseIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => CharacterPositionChecker.Verify(string.Empty, new[] { new CharacterPosition('J', 0) }));
            }

            [Test]
            public void WhenCharacterPositionsIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CharacterPositionChecker.Verify(PhraseString, null));
            }

            [Test]
            public void WhenCharacterPositionsIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => CharacterPositionChecker.Verify(PhraseString, Enumerable.Empty<CharacterPosition>()));
            }

            [Test]
            public void WhenPhraseContainsOneCharacter_ThenReturnTrue()
            {
                var result = CharacterPositionChecker.Verify(PhraseString, new[]
                {
                    new CharacterPosition('J', 0)
                });

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenPhraseContainsTwoCharacters_ThenReturnTrue()
            {
                var result = CharacterPositionChecker.Verify(PhraseString, new[]
                {
                    new CharacterPosition('J', 0),
                    new CharacterPosition('h', 2)
                });

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenPhraseDoesNotContainAllCharacters_ThenReturnFalse()
            {
                var result = CharacterPositionChecker.Verify(PhraseString, new[]
                {
                    new CharacterPosition('J', 0),
                    new CharacterPosition('X', 1),
                    new CharacterPosition('h', 2)
                });

                Assert.That(result, Is.False);
            }
        }

        [TestFixture]
        public class VerifyArray : CharacterPositionCheckerTests
        {
            [Test]
            public void WhenPhraseIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CharacterPositionChecker.Verify(null as char[], new[] { new CharacterPosition('J', 0) }));
            }

            [Test]
            public void WhenPhraseIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => CharacterPositionChecker.Verify(new char[0], new[] { new CharacterPosition('J', 0) }));
            }

            [Test]
            public void WhenCharacterPositionsIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CharacterPositionChecker.Verify(PhraseArray, null));
            }

            [Test]
            public void WhenCharacterPositionsIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => CharacterPositionChecker.Verify(PhraseArray, Enumerable.Empty<CharacterPosition>()));
            }

            [Test]
            public void WhenPhraseContainsOneCharacter_ThenReturnTrue()
            {
                var result = CharacterPositionChecker.Verify(PhraseArray, new[]
                {
                    new CharacterPosition('J', 0)
                });

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenPhraseContainsTwoCharacters_ThenReturnTrue()
            {
                var result = CharacterPositionChecker.Verify(PhraseArray, new[]
                {
                    new CharacterPosition('J', 0),
                    new CharacterPosition('h', 2)
                });

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenPhraseDoesNotContainAllCharacters_ThenReturnFalse()
            {
                var result = CharacterPositionChecker.Verify(PhraseArray, new[]
                {
                    new CharacterPosition('J', 0),
                    new CharacterPosition('X', 1),
                    new CharacterPosition('h', 2)
                });

                Assert.That(result, Is.False);
            }
        }
    }
}