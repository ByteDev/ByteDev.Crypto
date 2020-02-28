using ByteDev.Crypto.Encryption;
using ByteDev.Crypto.Encryption.Algorithms;
using ByteDev.Crypto.Encryption.KeyIv;
using NUnit.Framework;

namespace ByteDev.Crypto.Shared.PackageTests
{
    [TestFixture]
    public class EncryptionServiceTests
    {
        private const string ClearText = "john";
        private const string Password = "Password1";
        private const string Salt = "ftK9Q4f3";

        private IEncryptionKeyIvFactory _keyFactoryRijndael;

        private IEncryptionAlgorithm _rijndaelAlgo;

        protected EncryptionService CreateSut(IEncryptionAlgorithm algorithm, EncryptionKeyIv key)
        {
            return new EncryptionService(algorithm, key);
        }

        [SetUp]
        public void SetUp()
        {
            _rijndaelAlgo = new RijndaelAlgorithm();

            _keyFactoryRijndael = new EncryptionKeyIvFactory(new RijndaelAlgorithm());
        }

        [Test]
        public void WhenSourceAndSaltSupplied_ThenEncryptText()
        {
            var keyIv = _keyFactoryRijndael.Create(Password, Salt.GetBytes());

            var sut = CreateSut(_rijndaelAlgo, keyIv);

            var ciphertext = sut.Encrypt(ClearText);

            Assert.That(ciphertext, Is.Not.EqualTo(ClearText));
        }
    }
}