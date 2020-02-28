using System;
using ByteDev.Crypto.Encryption.Algorithms;
using ByteDev.Crypto.Encryption.KeyIv;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Encryption.KeyIv
{
    [TestFixture]
    public class EncryptionKeyIvFactoryTest
    {
        protected EncryptionKeyIvFactory CreateClassUnderTest()
        {
            return new EncryptionKeyIvFactory(new RijndaelAlgorithm());
        }

        protected byte[] GetBytes(string text)
        {
            return System.Text.Encoding.Default.GetBytes(text);
        }

        [TestFixture]
        public class CreateKeyIv : EncryptionKeyIvFactoryTest
        {
            [Test]
            public void WhenSaltLessThanEightBytes_ThenThrowException()
            {
                const string salt = "1234567";

                Assert.Throws<ArgumentException>(() => Act("Password123", GetBytes(salt)));
            }

            [Test]
            public void WhenSaltEightBytes_ThenReturnEncryptionKeyIv()
            {
                const string salt = "12345678";

                var result = Act("Password123", GetBytes(salt));

                Assert.That(result, Is.Not.Null);
            }

            [Test]
            public void WhenPasswordAndSalt_ThenSetKeyAndVi()
            {
                const string salt = "ftK9Q4f3sdf";

                var keyIv = Act("Password123", GetBytes(salt));

                Assert.That(keyIv.Key.Length, Is.EqualTo(32));      // 256 bit key
                Assert.That(keyIv.Iv.Length, Is.EqualTo(16));
            }

            private EncryptionKeyIv Act(string password, byte[] saltBytes)
            {
                var classUnderTest = CreateClassUnderTest();

                return classUnderTest.Create(password, saltBytes);
            }
        }
    }
}