using System;
using ByteDev.Crypto.Encryption;
using ByteDev.Crypto.Encryption.Algorithms;
using ByteDev.Crypto.Encryption.KeyIv;
using ByteDev.Encoding.Base32;
using ByteDev.Encoding.Base64;
using ByteDev.Encoding.Hex;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Encryption
{
    [TestFixture]
    public class EncryptionServiceTests
    {
        private const string ClearText = "john";
        private const string Password = "Password1";
        private const string Salt = "ftK9Q4f3";

        protected EncryptionService CreateSut(IEncryptionAlgorithm algorithm, EncryptionKeyIv key)
        {
            return new EncryptionService(algorithm, key);
        }

        [TestFixture]
        public class Constructor : EncryptionServiceTests
        {
            [Test]
            public void WhenKeyViIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new EncryptionService(new RijndaelAlgorithm(), null));
            }
        }

        [TestFixture]
        public class Encrypt : EncryptionServiceTests
        {
            private IEncryptionKeyIvFactory _keyFactoryRijndael;
            private IEncryptionKeyIvFactory _keyFactoryTripleDes;

            private IEncryptionAlgorithm _rijndaelAlgo;
            private IEncryptionAlgorithm _tripleDesAlgo;

            [SetUp]
            public void SetUp()
            {
                _rijndaelAlgo = new RijndaelAlgorithm();
                _tripleDesAlgo = new TripleDesAlgorithm();

                _keyFactoryRijndael = new EncryptionKeyIvFactory(new RijndaelAlgorithm());
                _keyFactoryTripleDes = new EncryptionKeyIvFactory(new TripleDesAlgorithm());
            }

            [Test]
            public void WhenClearTextIsNull_ThenThrowException()
            {
                var keyIv = _keyFactoryRijndael.Create(Password, Salt.GetBytes());

                var sut = CreateSut(_rijndaelAlgo, keyIv);

                Assert.Throws<ArgumentException>(() => sut.Encrypt(null));
            }

            [Test]
            public void WhenSourceAndSaltSupplied_ThenEncryptText()
            {
                var keyIv = _keyFactoryRijndael.Create(Password, Salt.GetBytes());

                var sut = CreateSut(_rijndaelAlgo, keyIv);

                var ciphertext = sut.Encrypt(ClearText);

                Assert.That(ciphertext, Is.Not.EqualTo(ClearText));
            }

            [Test]
            public void WhenDifferentIv_ThenCreateDifferentCipherTexts()
            {
                byte[] key = { 123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };

                byte[] vi1 = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 155 };
                byte[] vi2 = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 252, 112, 79, 32, 114, 156 };

                var sut = CreateSut(_rijndaelAlgo, new EncryptionKeyIv(key, vi1));
                var sut2 = CreateSut(_rijndaelAlgo, new EncryptionKeyIv(key, vi2));

                var cipherText1 = sut.Encrypt(ClearText);
                var cipherText2 = sut2.Encrypt(ClearText);

                Assert.That(cipherText1, Is.Not.EqualTo(cipherText2));
            }

            [Test]
            public void WhenDifferentAlgorithms_ThenDifferentCiphers()
            {
                var keyIvRij = _keyFactoryRijndael.Create(Password, Salt.GetBytes());
                var keyIvTDes = _keyFactoryTripleDes.Create(Password, Salt.GetBytes());

                var sutRij = CreateSut(_rijndaelAlgo, keyIvRij);
                var sutTripleDes = CreateSut(_tripleDesAlgo, keyIvTDes);

                var cipherText1 = sutRij.Encrypt(ClearText);
                var cipherText2 = sutTripleDes.Encrypt(ClearText);

                Assert.That(cipherText1, Is.Not.EqualTo(cipherText2));
            }
        }

        [TestFixture]
        public class EncryptProperties : EncryptionServiceTests
        {
            private EncryptionService _sut;

            [SetUp]
            public void SetUp()
            {
                var algo = new RijndaelAlgorithm();

                var keyFactoryRijndael = new EncryptionKeyIvFactory(algo);

                var keyIv = keyFactoryRijndael.Create(Password, Salt.GetBytes());

                _sut = CreateSut(algo, keyIv);
            }

            [Test]
            public void WhenObjectIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.EncryptProperties(null));
            }

            [Test]
            public void WhenTypeHasNoAttributes_ThenDontEncryptProperties()
            {
                var obj = TestNoAttributes.Create();

                _sut.EncryptProperties(obj);

                Assert.That(obj.Name, Is.EqualTo("John"));
                Assert.That(obj.Age, Is.EqualTo(50));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenStringPropertyWithAttributeIsNullOrEmpty_ThenDontEncrypt(string value)
            {
                var obj = TestHasAttributes.Create();

                obj.Name = value;

                _sut.EncryptProperties(obj);

                Assert.That(obj.Name, Is.EqualTo(value));
            }

            [Test]
            public void WhenTypeHasAttributes_ThenEncryptStringPropertiesWithAttribute()
            {
                var obj = TestHasAttributes.Create();

                _sut.EncryptProperties(obj);

                Assert.That(obj.Name, Is.Not.EqualTo("John"));
                Assert.That(obj.Address, Is.Not.EqualTo("Somewhere"));

                Assert.That(obj.Country, Is.EqualTo("UK"));
                Assert.That(obj.Age, Is.EqualTo(50));
            }

            [Test]
            public void WhenEncodingTypeBase64_ThenEncodeToBase64()
            {
                var obj = TestHasAttributes.Create();

                _sut.EncryptProperties(obj, EncodingType.Base64);

                Assert.That(obj.Name.IsBase64(), Is.True);
                Assert.That(obj.Address.IsBase64(), Is.True);
            }

            [Test]
            public void WhenEncodingTypeHex_ThenEncodeToHex()
            {
                var obj = TestHasAttributes.Create();

                _sut.EncryptProperties(obj, EncodingType.Hex);

                Assert.That(obj.Name.IsHex(), Is.True);
                Assert.That(obj.Address.IsHex(), Is.True);
            }

            [Test]
            public void WhenEncodingTypeBase32_ThenEncodeToBase32()
            {
                var obj = TestHasAttributes.Create();

                _sut.EncryptProperties(obj, EncodingType.Base32);

                Assert.That(obj.Name.IsBase32(), Is.True);
                Assert.That(obj.Address.IsBase32(), Is.True);
            }
        }

        [TestFixture]
        public class Decrypt : EncryptionServiceTests
        {
            private EncryptionService _sut;

            [SetUp]
            public void SetUp()
            {
                var algo = new RijndaelAlgorithm();
                var encryptionKeyIvFactory = new EncryptionKeyIvFactory(algo);
                
                var keyIv = encryptionKeyIvFactory.Create(Password, Salt.GetBytes());
                
                _sut = CreateSut(algo, keyIv);
            }

            [Test]
            public void WhenCipherIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.Decrypt(null));
            }

            [Test]
            public void WhenEncryptedText_ThenDecryptText()
            {
                byte[] cipher = _sut.Encrypt(ClearText);

                var result = _sut.Decrypt(cipher);

                Assert.That(ClearText, Is.EqualTo(result));
            }
        }

        [TestFixture]
        public class DecryptProperties : EncryptionServiceTests
        {
            private EncryptionService _sut;

            [SetUp]
            public void SetUp()
            {
                var algo = new RijndaelAlgorithm();

                var keyFactoryRijndael = new EncryptionKeyIvFactory(algo);

                var keyIv = keyFactoryRijndael.Create(Password, Salt.GetBytes());

                _sut = CreateSut(algo, keyIv);
            }

            [Test]
            public void WhenObjectIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.DecryptProperties(null));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenPropertyWithAttributeIsNullOrEmpty_ThenDoNotDecrypt(string value)
            {
                var obj = TestHasAttributes.Create();

                _sut.EncryptProperties(obj);

                obj.Name = value;

                _sut.DecryptProperties(obj);

                Assert.That(obj.Name, Is.EqualTo(value));
            }

            [Test]
            public void WhenEncodingIsBase64_ThenDecryptStringPropertiesWithAttribute()
            {
                var obj = TestHasAttributes.Create();

                _sut.EncryptProperties(obj);

                _sut.DecryptProperties(obj);

                Assert.That(obj.Name, Is.EqualTo("John"));
                Assert.That(obj.Address, Is.EqualTo("Somewhere"));
                Assert.That(obj.Country, Is.EqualTo("UK"));
                Assert.That(obj.Age, Is.EqualTo(50));
            }

            [Test]
            public void WhenEncodingIsHex_ThenDecryptStringPropertiesWithAttribute()
            {
                var obj = TestHasAttributes.Create();

                _sut.EncryptProperties(obj, EncodingType.Base32);

                _sut.DecryptProperties(obj, EncodingType.Base32);

                Assert.That(obj.Name, Is.EqualTo("John"));
                Assert.That(obj.Address, Is.EqualTo("Somewhere"));
                Assert.That(obj.Country, Is.EqualTo("UK"));
                Assert.That(obj.Age, Is.EqualTo(50));
            }
        }
    }
}
