using System;
using ByteDev.Crypto.Hashing;
using ByteDev.Crypto.Hashing.Algorithms;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests.Hashing
{
    [TestFixture]
    public class FileChecksumServiceTests
    {
        private IFileChecksumService _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new FileChecksumService();
        }

        [TestFixture]
        public class Constructor : FileChecksumServiceTests
        {
            [Test]
            public void WhenHashAlgorithmIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new FileChecksumService(null));
            }

            [Test]
            public void WhenEncodingTypeInvalid_ThenThrowException()
            {
                Assert.Throws<InvalidOperationException>(() => _ = new FileChecksumService(new Md5Algorithm(), (EncodingType)99));
            }
        }

        [TestFixture]
        public class Calculate : FileChecksumServiceTests
        {
            [Test]
            public void WhenFilePathIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Calculate(null));
            }

            [Test]
            public void WhenFilePathIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.Calculate(string.Empty));
            }
        }

        [TestFixture]
        public class Calculate_WithBufferSize : FileChecksumServiceTests
        {
            [Test]
            public void WhenFilePathIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Calculate(null, 1));
            }

            [Test]
            public void WhenFilePathIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.Calculate(string.Empty, 1));
            }

            [Test]
            public void WhenBufferSpecifiedAsLessThanOne_ThenThrowException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(@"C:\somefile", 0));
            }
        }

        [TestFixture]
        public class Verify : FileChecksumServiceTests
        {
            [Test]
            public void WhenFilePathIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Verify(null, "expectedChecksum"));
            }

            [Test]
            public void WhenFilePathIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.Verify(string.Empty, "expectedChecksum"));
            }

            [Test]
            public void WhenExpectedChecksumIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Verify(@"C:\file.txt", null));
            }
        }

        [TestFixture]
        public class Verify_WithBufferSize : FileChecksumServiceTests
        {
            [Test]
            public void WhenFilePathIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Verify(null, "expectedChecksum", 1));
            }

            [Test]
            public void WhenFilePathIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.Verify(string.Empty, "expectedChecksum", 1));
            }

            [Test]
            public void WhenExpectedChecksumIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Verify(@"C:\file.txt", null, 1));
            }

            [Test]
            public void WhenBufferSpecifiedAsLessThanOne_ThenThrowException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Verify(@"C:\file.txt", "checksum", 0));
            }
        }

        [TestFixture]
        public class Matches : FileChecksumServiceTests
        {
            [Test]
            public void WhenDirIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Matches(null, "checksum"));
            }

            [Test]
            public void WhenDirIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => _sut.Matches(string.Empty, "checksum"));
            }

            [Test]
            public void WhenChecksumIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _sut.Matches(@"C:\file.txt", null));
            }
        }
    }
}