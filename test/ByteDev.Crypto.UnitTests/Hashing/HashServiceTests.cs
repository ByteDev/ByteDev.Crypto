using System;
using ByteDev.Common.Encoding;
using ByteDev.Crypto.Hashing;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class HashServiceTests
    {
        private static HashService CreateSut()
        {
            return new HashService();
        }
        
        [TestFixture]
        public class Hash : HashServiceTests
        {
            [Test]
            public void WhenPhraseIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => Act(null, string.Empty));
            }

            [Test]
            public void WhenPhraseAndNoSalt_ThenReturnBase64Hash()
            {
                const string phrase = "smith";

                var result = Act(phrase, string.Empty);

                Assert.AreNotEqual(phrase, result);
                Assert.That(Base64.IsBase64Encoded(result), Is.True);
            }

            [Test]
            public void WhenPhraseAndNoSalt_WhenCalledTwiceWithSamePhrase_ThenReturnEqualHashs()
            {
                const string phrase = "smith";

                var result1 = Act(phrase, string.Empty);
                var result2 = Act(phrase, string.Empty);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenPhraseAndSalt_ThenAddSaltToHash()
            {
                const string phrase = "smith";
                const string salt = "some salt";

                var resultSalted = Act(phrase, salt);
                var resultNotSalted = Act(phrase, string.Empty);

                Assert.That(resultSalted, Is.Not.EqualTo(resultNotSalted));
            }
            
            private string Act(string phrase, string salt)
            {
                var sut = CreateSut();

                return sut.Hash(new HashPhrase(phrase, salt));
            }
        }

        [TestFixture]
        public class Verify : HashServiceTests
        {
            private HashService _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = CreateSut();
            }

            [Test]
            public void WhenPhraseIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => Act(null, string.Empty, "someHashedPhrase"));
            }

            [Test]
            public void WhenExpectedHashedPhraseIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => Act("smith", string.Empty, null));
            }

            [Test]
            public void WhenHashPhraseIsDerivedFromSamePhrase_WithSalt_ThenReturnTrue()
            {
                const string phrase = "smith";
                const string salt = "some salt";

                var hashedPhrase = HashPhraseWithSalt(phrase, salt);

                var result = Act(phrase, salt, hashedPhrase);

                Assert.That(result, Is.True);
            }

            [Test]
            public void WhenHashPhraseIsNotDerivedFromSamePhrase_WithSalt_ThenReturnFalse()
            {
                const string phrase = "smith";
                const string salt = "some salt";

                var hashedPhrase = HashPhraseWithSalt(phrase, salt);

                var result = Act("user phrase", salt, hashedPhrase);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenPhraseIsEmpty_ThenReturnFalse()
            {
                const string hashedPhrase = "someHashedPhrase";

                var result = Act(string.Empty, string.Empty, hashedPhrase);

                Assert.That(result, Is.False);
            }

            private string HashPhraseWithSalt(string phrase, string salt)
            {
                return _sut.Hash(new HashPhrase(phrase, salt));
            }

            private bool Act(string phrase, string salt, string hashedPhrase)
            {
                return _sut.Verify(new HashPhrase(phrase, salt), hashedPhrase);
            }
        }

        [TestFixture]
        public class CalcFileChecksum : HashServiceTests
        {
            [Test]
            public void WhenFilePathIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CreateSut().CalcFileChecksum(null));
            }

            [Test]
            public void WhenFilePathIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => CreateSut().CalcFileChecksum(string.Empty));
            }
        }
    }
}
