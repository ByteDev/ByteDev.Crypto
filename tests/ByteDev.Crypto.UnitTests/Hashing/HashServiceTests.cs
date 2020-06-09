using System;
using ByteDev.Base64;
using ByteDev.Crypto.Hashing;
using ByteDev.Crypto.Hashing.Algorithms;
using ByteDev.Strings;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class HashServiceTests
    {
        [TestFixture]
        public class Hash : HashServiceTests
        {
            private static HashService CreateSut(EncodingType encoding = EncodingType.Base64)
            {
                return new HashService(new Md5Algorithm(), encoding);
            }

            [Test]
            public void WhenPhraseIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => CreateSut().Hash(null));
            }

            [Test]
            public void WhenEncodingIsBase64_ThenReturnBase64Hash()
            {
                const string phrase = "smith";

                var result = CreateSut().Hash(new ClearPhrase(phrase));

                Assert.That(phrase, Is.Not.EqualTo(result));
                Assert.That(result.IsBase64(), Is.True);
            }

            [Test]
            public void WhenEncodingIsHex_ThenReturnHexHash()
            {
                const string phrase = "smith";

                var result = CreateSut(EncodingType.Hex).Hash(new ClearPhrase(phrase));

                Assert.That(result, Is.Not.EqualTo(phrase));
                Assert.That(result.IsHex(), Is.True);
            }

            [Test]
            public void WhenPhraseAndNoSalt_WhenCalledTwiceWithSamePhrase_ThenReturnEqualHashs()
            {
                const string phrase = "smith";

                var sut = CreateSut();

                var result1 = sut.Hash(new ClearPhrase(phrase));
                var result2 = sut.Hash(new ClearPhrase(phrase));

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenPhraseAndSalt_ThenAddSaltToHash()
            {
                const string phrase = "smith";
                const string salt = "some salt";

                var sut = CreateSut();

                var resultSalted = sut.Hash(new ClearPhrase(phrase, salt));
                var resultNotSalted = sut.Hash(new ClearPhrase(phrase));

                Assert.That(resultSalted, Is.Not.EqualTo(resultNotSalted));
            }
        }

        [TestFixture]
        public class Verify : HashServiceTests
        {
            private HashService _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new HashService();
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
                return _sut.Hash(new ClearPhrase(phrase, salt));
            }

            private bool Act(string phrase, string salt, string hashedPhrase)
            {
                return _sut.Verify(new ClearPhrase(phrase, salt), hashedPhrase);
            }
        }

        [TestFixture]
        public class CalcFileChecksum : HashServiceTests
        {
            [Test]
            public void WhenFilePathIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => new HashService().CalcFileChecksum(null));
            }

            [Test]
            public void WhenFilePathIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => new HashService().CalcFileChecksum(string.Empty));
            }
        }
    }
}
