using System;
using ByteDev.Crypto.Hashing;
using ByteDev.Crypto.Hashing.Algorithms;
using ByteDev.Encoding.Base32;
using ByteDev.Encoding.Base64;
using ByteDev.Encoding.Hex;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class HashServiceTests
    {
        [TestFixture]
        public class Constructor : HashServiceTests
        {
            [Test]
            public void WhenHashAlgorithmIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new HashService(null));
            }

            [Test]
            public void WhenEncodingTypeInvalid_ThenThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => _ = new HashService(new Md5Algorithm(), (EncodingType)99));
            }
        }

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
            public void WhenEncodingIsBase32_ThenReturnBase32Hash()
            {
                const string phrase = "smith";

                var result = CreateSut(EncodingType.Base32).Hash(new ClearPhrase(phrase));

                Assert.That(result, Is.Not.EqualTo(phrase));
                Assert.That(result.IsBase32(), Is.True);
            }

            [Test]
            public void WhenCalledTwiceWithSamePhrase_ThenReturnEqualHashs()
            {
                var sut = CreateSut();

                var phrase = new ClearPhrase("Hello", "World", "Everyone!");

                var result1 = sut.Hash(phrase);
                var result2 = sut.Hash(phrase);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenHasDifferentSalt_ThenReturnNotEqualHashes()
            {
                const string phrase = "smith";

                var sut = CreateSut();

                var result1 = sut.Hash(new ClearPhrase(phrase, "some salt"));
                var result2 = sut.Hash(new ClearPhrase(phrase, "some different salt"));

                Assert.That(result1, Is.Not.EqualTo(result2));
            }

            [Test]
            public void WhenHasDifferentPepper_ThenReturnNotEqualHashes()
            {
                const string phrase = "smith";
                const string salt = "some salt";
                
                var sut = CreateSut();

                var result1 = sut.Hash(new ClearPhrase(phrase, salt, "some pepper"));
                var result2 = sut.Hash(new ClearPhrase(phrase, salt, "some different pepper"));

                Assert.That(result1, Is.Not.EqualTo(result2));
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

            [TestCase("61409AA1FD47D4A5332DE23CBF59A36F")]
            [TestCase("61409aa1fd47d4a5332de23cbf59a36f")]
            public void WhenCorrectHexHash_AndDifferentCase_ThenReturnTrue(string expectedHash)
            {
                var sut = new HashService(new Md5Algorithm(), EncodingType.Hex);

                var result = sut.Verify(new ClearPhrase("John"), expectedHash);

                Assert.That(result, Is.True);
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
    }
}
