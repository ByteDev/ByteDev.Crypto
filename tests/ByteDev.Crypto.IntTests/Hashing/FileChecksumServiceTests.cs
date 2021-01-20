using System.IO;
using ByteDev.Crypto.Hashing;
using ByteDev.Crypto.Hashing.Algorithms;
using NUnit.Framework;

namespace ByteDev.Crypto.IntTests.Hashing
{
    [TestFixture]
    public class FileChecksumServiceTests
    {
        private const string NotExistsFile = @"C:\thisdoesnotexist.txt";

        private const string TestFile1 = @"Hashing\TestFile1.txt";
        private const string TestFile2 = @"Hashing\TestFile2.txt";

        private const int TestFile1Size = 23;    // in bytes

        private FileChecksumService _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new FileChecksumService(new Md5Algorithm(), EncodingType.Base64);
        }

        [TestFixture]
        public class Calculate : FileChecksumServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sut.Calculate(NotExistsFile));
            }

            [Test]
            public void WhenFileExists_ThenReturnsCheckSum()
            {
                var result = _sut.Calculate(TestFile1);

                Assert.That(result, Is.EqualTo("X4eScXhJRrCT1CS2N7Om+Q=="));
            }

            [Test]
            public void WhenFileContentsAreEqual_ThenReturnsEqualChecksum()
            {
                var result1 = _sut.Calculate(TestFile1);
                var result2 = _sut.Calculate(TestFile1);

                Assert.That(result1, Is.EqualTo(result2));
            }
        }

        [TestFixture]
        public class Calculate_WithBuffer : FileChecksumServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sut.Calculate(@"C:\thisdoesnotexist.txt", 1));
            }

            [TestCase(1)]
            [TestCase(2)]
            [TestCase(5)]
            [TestCase(12)]
            public void WhenBufferGreaterThanZero_ThenCheckOnlyThatNumberOfBytes(int size)
            {
                var result1 = _sut.Calculate(TestFile1, size);
                var result2 = _sut.Calculate(TestFile2, size);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenBufferSameSizeAsFileSize_ThenReturnEqualCheckSum()
            {
                var result1 = _sut.Calculate(TestFile1);
                var result2 = _sut.Calculate(TestFile1, TestFile1Size);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenBufferGreaterThanFileSize_ThenReturnEqualCheckSum()
            {
                var result1 = _sut.Calculate(TestFile1);
                var result2 = _sut.Calculate(TestFile1, TestFile1Size + 2);

                Assert.That(result1, Is.EqualTo(result2));   
            }
        }

        [TestFixture]
        public class Verify : FileChecksumServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sut.Verify(NotExistsFile, "expected"));
            }

            [Test]
            public void WhenExpectedChecksumNotCorrect_ThenReturnFalse()
            {
                const string checksum = "notCorrect";

                var result = _sut.Verify(TestFile1, checksum);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenExpectedChecksumCorrect_ThenReturnTrue()
            {
                var expectedChecksum = _sut.Calculate(TestFile1);

                var result = _sut.Verify(TestFile1, expectedChecksum);

                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class Verify_WithBuffer : FileChecksumServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sut.Verify(NotExistsFile, "expected", 1));
            }

            [Test]
            public void WhenExpectedChecksumNotCorrect_ThenReturnFalse()
            {
                const string checksum = "notCorrect";

                var result = _sut.Verify(TestFile1, checksum, 12);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenExpectedChecksumCorrect_ThenReturnTrue()
            {
                var expectedChecksum = _sut.Calculate(TestFile1, 12);

                var result = _sut.Verify(TestFile1, expectedChecksum, 12);

                Assert.That(result, Is.True);
            }
        }
    }
}