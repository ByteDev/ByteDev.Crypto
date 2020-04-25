using System;
using ByteDev.Crypto.Hashing;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class HashPhraseTests
    {
        [Test]
        public void WhenPhraseIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new HashPhrase(null));
        }

        [Test]
        public void WhenSaltIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new HashPhrase("phrase", null));
        }

        [Test]
        public void WhenPepperIsNull_ThenThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new HashPhrase("phrase", "salt", null));
        }

        [Test]
        public void WhenArgsAreValid_ThenSetProperties()
        {
            const string phrase = "phrase";
            const string salt = "salt";
            const string pepper = "pepper";

            var sut = new HashPhrase(phrase, salt, pepper);

            Assert.That(sut.Phrase, Is.EqualTo(phrase));
            Assert.That(sut.Salt, Is.EqualTo(salt));
            Assert.That(sut.Pepper, Is.EqualTo(pepper));
        }
    }
}