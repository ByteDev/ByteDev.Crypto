using System.IO;
using System.Linq;
using ByteDev.Collections;
using ByteDev.Crypto.Hashing;
using ByteDev.Crypto.Hashing.Algorithms;
using ByteDev.Encoding.Hex;
using NUnit.Framework;

namespace ByteDev.Crypto.IntTests.Hashing
{
    [TestFixture]
    public class FileChecksumServiceTests
    {
        private const string NotExistsFile = @"C:\thisdoesnotexist.txt";
        private const string NotExistsDir = @"F:\4c02319c9d774c5fb022debc753eadc7";

        private const string TestDir = "Hashing";
        private const string TestFile1 = @"Hashing\TestFile1.txt";
        private const string TestFile2 = @"Hashing\TestFile2.txt";
        private const string TestFile2Equals = @"Hashing\TestFile2equals.txt";

        private const int TestFile1Size = 23;    // in bytes

        private FileChecksumService _sutBase64;
        private FileChecksumService _sutHex;

        [SetUp]
        public void SetUp()
        {
            _sutBase64 = new FileChecksumService(new Md5Algorithm(), EncodingType.Base64);
            _sutHex = new FileChecksumService(new Md5Algorithm(), EncodingType.Hex);
        }

        [TestFixture]
        public class Calculate : FileChecksumServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sutBase64.Calculate(NotExistsFile));
            }

            [Test]
            public void WhenFileExists_ThenReturnsCheckSum()
            {
                var result = _sutBase64.Calculate(TestFile1);

                Assert.That(result, Is.EqualTo("X4eScXhJRrCT1CS2N7Om+Q=="));
            }

            [Test]
            public void WhenFileContentsAreEqual_ThenReturnsEqualChecksum()
            {
                var result1 = _sutBase64.Calculate(TestFile1);
                var result2 = _sutBase64.Calculate(TestFile1);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenUsingHex_ThenReturnUpperCase()
            {
                var result = _sutHex.Calculate(TestFile1);

                Assert.That(result.IsHex(), Is.True);
            }
        }

        [TestFixture]
        public class Calculate_WithBuffer : FileChecksumServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sutBase64.Calculate(@"C:\thisdoesnotexist.txt", 1));
            }

            [TestCase(1)]
            [TestCase(2)]
            [TestCase(5)]
            [TestCase(12)]
            public void WhenBufferGreaterThanZero_ThenCheckOnlyThatNumberOfBytes(int size)
            {
                var result1 = _sutBase64.Calculate(TestFile1, size);
                var result2 = _sutBase64.Calculate(TestFile2, size);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenBufferSameSizeAsFileSize_ThenReturnEqualCheckSum()
            {
                var result1 = _sutBase64.Calculate(TestFile1);
                var result2 = _sutBase64.Calculate(TestFile1, TestFile1Size);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenBufferGreaterThanFileSize_ThenReturnEqualCheckSum()
            {
                var result1 = _sutBase64.Calculate(TestFile1);
                var result2 = _sutBase64.Calculate(TestFile1, TestFile1Size + 2);

                Assert.That(result1, Is.EqualTo(result2));   
            }

            [Test]
            public void WhenUsingHex_ThenReturnUpperCase()
            {
                var result = _sutHex.Calculate(TestFile1, 12);

                Assert.That(result.IsHex(), Is.True);
            }
        }

        [TestFixture]
        public class Verify : FileChecksumServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sutBase64.Verify(NotExistsFile, "expected"));
            }

            [Test]
            public void WhenExpectedChecksumNotCorrect_ThenReturnFalse()
            {
                const string checksum = "notCorrect";

                var result = _sutBase64.Verify(TestFile1, checksum);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenExpectedChecksumCorrect_ThenReturnTrue()
            {
                var expectedChecksum = _sutBase64.Calculate(TestFile1);

                var result = _sutBase64.Verify(TestFile1, expectedChecksum);

                Assert.That(result, Is.True);
            }
            
            [TestCase("5F879271784946B093D424B637B3A6F9")]
            [TestCase("5f879271784946b093d424b637b3a6f9")]
            public void WhenExpectedChecksumHex_ThenReturnTrue(string expectedChecksum)
            {
                var result = _sutHex.Verify(TestFile1, expectedChecksum);

                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class Verify_WithBuffer : FileChecksumServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sutBase64.Verify(NotExistsFile, "expected", 1));
            }

            [Test]
            public void WhenExpectedChecksumNotCorrect_ThenReturnFalse()
            {
                const string checksum = "notCorrect";

                var result = _sutBase64.Verify(TestFile1, checksum, 12);

                Assert.That(result, Is.False);
            }

            [Test]
            public void WhenExpectedChecksumCorrect_ThenReturnTrue()
            {
                var expectedChecksum = _sutBase64.Calculate(TestFile1, 12);

                var result = _sutBase64.Verify(TestFile1, expectedChecksum, 12);

                Assert.That(result, Is.True);
            }
        }

        [TestFixture]
        public class Matches : FileChecksumServiceTests
        {
            [Test]
            public void WhenDirDoesNotExist_ThenThrowException()
            {
                Assert.Throws<DirectoryNotFoundException>(() => _sutBase64.Matches(NotExistsDir, "checksum"));
            }

            [Test]
            public void WhenDirDoesNotContainAnyMatches_ThenReturnEmpty()
            {
                var result = _sutBase64.Matches(TestDir, "nonMatchingChecksum");

                Assert.That(result, Is.Empty);
            }

            [Test]
            public void WhenDirContainsSingleMatch_ThenReturnSingle()
            {
                var checksum = _sutBase64.Calculate(TestFile1);

                var result = _sutBase64.Matches(TestDir, checksum);

                Assert.That(result.Single(), Is.EqualTo(TestFile1));
            }

            [Test]
            public void WhenDirContainsTwoMatches_ThenReturnTwo()
            {
                var checksum = _sutBase64.Calculate(TestFile2);

                var result = _sutBase64.Matches(TestDir, checksum);

                Assert.That(result.First(), Is.EqualTo(TestFile2));
                Assert.That(result.Second(), Is.EqualTo(TestFile2Equals));
            }

            [TestCase("5F879271784946B093D424B637B3A6F9")]
            [TestCase("5f879271784946b093d424b637b3a6f9")]
            public void WhenHashIsHex_AndSingleMatch_ThenReturnSingle(string checksum)
            {
                var result = _sutHex.Matches(TestDir, checksum);

                Assert.That(result.Single(), Is.EqualTo(TestFile1));
            }
        }
    }
}