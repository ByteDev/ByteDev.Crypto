using System;
using System.Diagnostics;
using System.IO;
using ByteDev.Crypto.Hashing;
using ByteDev.Crypto.Hashing.Algorithms;
using NUnit.Framework;

namespace ByteDev.Crypto.IntTests.Hashing
{
    [TestFixture]
    public class HashServiceTests
    {
        [TestFixture]
        public class CalcFileChecksum
        {
            private const string TestFile1 = @"Hashing\TestFile1.txt";
            private const string TestFile2 = @"Hashing\TestFile2.txt";

            private HashService _sut;

            [SetUp]
            public void SetUp()
            {
                _sut = new HashService(new Md5Algorithm());
            }

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
                var result2 = _sut.CalcFileChecksum(TestFile2);

                Assert.That(result1, Is.EqualTo(result2));
            }

            [Test]
            [Ignore("Takes a minute to run")]
            public void WhenLargeFile_ThenTimeAllAlgos()
            {
                TimeOperation(new Sha1Algorithm(), "SHA1");
                TimeOperation(new Md5Algorithm(), "MD5");
                TimeOperation(new Sha512Algorithm(), "SHA512");
                TimeOperation(new Sha256Algorithm(), "SHA256");
                TimeOperation(new HmacSha256Algorithm("someKey"), "HMACSHA256");

                /*
                // File: 4.6GB
                SHA1: 00:00:10.4115546
                MD5: 00:00:11.0690976
                SHA512: 00:00:14.1424741
                SHA256: 00:00:21.6134517
                HMACSHA256: 00:00:21.5468394
                */
            }

            private static void TimeOperation(IHashAlgorithm algo, string messagePrefix)
            {
                const string bigFile = @"C:\Video\1.mkv";    // 4.6GB

                var timer = new Stopwatch();
                timer.Start();
                new HashService(algo).CalcFileChecksum(bigFile);
                timer.Stop();

                Console.WriteLine($"{messagePrefix}: " + timer.Elapsed);
            }
        }
    }
}