using System;
using NUnit.Framework;

namespace ByteDev.Crypto.UnitTests
{
    [TestFixture]
    public class CharacterPositionTests
    {
        [Test]
        public void WhenPositionLessThanZero_ThenThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CharacterPosition('A', -1));
        }

        [Test]
        public void WhenParamsValid_ThenSetProperties()
        {
            var sut = new CharacterPosition('A', 0);

            Assert.That(sut.Character, Is.EqualTo('A'));
            Assert.That(sut.Position, Is.EqualTo(0));
        }
    }
}