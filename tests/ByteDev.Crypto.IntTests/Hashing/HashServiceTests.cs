using System.IO;
using ByteDev.Crypto.Hashing;
using ByteDev.Crypto.Hashing.Algorithms;
using NUnit.Framework;

namespace ByteDev.Crypto.IntTests.Hashing
{
    [TestFixture]
    public class HashServiceTests
    {
        private const string TestFile1 = @"Hashing\TestFile1.txt";
        private const string TestFile2 = @"Hashing\TestFile2.txt";

        private const int TestFile1Size = 23;    // in bytes

        private HashService _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HashService(new Md5Algorithm());
        }

        [TestFixture]
        public class CalcFileChecksum : HashServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sut.CalcFileChecksum(@"C:\thisdoesnotexist.txt"));
            }

            [Test]
            public void WhenFileExists_ThenReturnsCheckSum()
            {
                var result = _sut.CalcFileChecksum(TestFile1);

                Assert.That(result, Is.EqualTo("X4eScXhJRrCT1CS2N7Om+Q=="));
            }

            [Test]
            public void WhenFileContentsAreEqual_ThenReturnsEqualChecksum()
            {
                var result1 = _sut.CalcFileChecksum(TestFile1);
                var result2 = _sut.CalcFileChecksum(TestFile1);

                Assert.That(result1, Is.EqualTo(result2));
            }
        }

        [TestFixture]
        public class CalcFileChecksum_WithBuffer : HashServiceTests
        {
            [Test]
            public void WhenFileDoesNotExist_ThenThrowException()
            {
                Assert.Throws<FileNotFoundException>(() => _sut.CalcFileChecksum(@"C:\thisdoesnotexist.txt", 1));
            }

            [TestCase(1)]
            [TestCase(2)]
            [TestCase(5)]
            [TestCase(12)]
            public void WhenBufferGreaterThanZero_ThenCheckOnlyThatNumberOfBytes(int size)
            {
                var result1 = _sut.CalcFileChecksum(TestFile1, size);
                var result2 = _sut.CalcFileChecksum(TestFile2, size);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenBufferSameSizeAsFileSize_ThenReturnEqualCheckSum()
            {
                var result1 = _sut.CalcFileChecksum(TestFile1);
                var result2 = _sut.CalcFileChecksum(TestFile1, TestFile1Size);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            public void WhenBufferGreaterThanFileSize_ThenReturnEqualCheckSum()
            {
                var result1 = _sut.CalcFileChecksum(TestFile1);
                var result2 = _sut.CalcFileChecksum(TestFile1, TestFile1Size + 2);

                Assert.That(result1, Is.EqualTo(result2));   
            }
        }
    }
}