using System;
using System.Text;
using ByteDev.Crypto.Hashing;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class ClearPhraseTests
    {
        [TestFixture]
        public class Constructor : ClearPhraseTests
        {
            [Test]
            public void WhenPhraseIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new ClearPhrase(null));
            }

            [Test]
            public void WhenSaltIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new ClearPhrase("phrase", null));
            }

            [Test]
            public void WhenPepperIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new ClearPhrase("phrase", "salt", null));
            }

            [Test]
            public void WhenArgsAreValid_ThenSetProperties()
            {
                const string phrase = "phrase";
                const string salt = "salt";
                const string pepper = "pepper";

                var sut = new ClearPhrase(phrase, salt, pepper);

                Assert.That(sut.Phrase, Is.EqualTo(phrase));
                Assert.That(sut.Salt, Is.EqualTo(salt));
                Assert.That(sut.Pepper, Is.EqualTo(pepper));
            }
        }

        [TestFixture]
        public class Encoding : ClearPhraseTests
        {
            [Test]
            public void WhenEncodingIsNotSet_ThenReturnUtf8()
            {
                var sut = new ClearPhrase("some phrase");
                
                Assert.That(sut.Encoding, Is.Not.Null);
                Assert.That(sut.Encoding, Is.TypeOf<UTF8Encoding>());
            }

            [Test]
            public void WhenEncodingIsNull_ThenReturnUtf8()
            {
                var sut = new ClearPhrase("some phrase")
                {
                    Encoding = null
                };
                
                Assert.That(sut.Encoding, Is.Not.Null);
                Assert.That(sut.Encoding, Is.TypeOf<UTF8Encoding>());
            }

            [Test]
            public void WhenEncodingSet_ThenReturnSetType()
            {
                var encoding = new ASCIIEncoding();

                var sut = new ClearPhrase("some phrase")
                {
                    Encoding = encoding
                };
                
                Assert.That(sut.Encoding, Is.SameAs(encoding));
            }
        }
    }
}