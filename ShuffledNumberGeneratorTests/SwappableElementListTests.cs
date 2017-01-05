using NUnit.Framework;
using ShuffledNumberGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffledNumberGenerator.Tests
{
    [TestFixture]
    public class SwappableElementListTests
    {
        private SwappableElementList<int> _subject;

        [SetUp]
        public void SetUp()
        {
            _subject = new SwappableElementList<int>(5);
            for (var i = 0; i < 5; ++i)
            {
                _subject.Add(i);
            }
        }

        [Test]
        public void SwapNegativeFirstIndexTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.SwapElements(-1, 0));
        }

        [Test]
        public void SwapFirstIndexSizeOfListTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.SwapElements(5, 0));
        }

        [Test]
        public void SwapFirstIndexLargerThanSizeOfListTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.SwapElements(10, 0));
        }

        [Test]
        public void SwapNegativeSecondIndexTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.SwapElements(0, -1));
        }

        [Test]
        public void SwapSecondIndexSizeOfListTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.SwapElements(0, 5));
        }
        
        [Test]
        public void SwapSecondIndexLargerThanSizeOfListTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _subject.SwapElements(0, 10));
        }

        [Test]
        public void SwapSameElementTest()
        {
            _subject.SwapElements(0, 0);
            Assert.AreEqual(new List<int> { 0, 1, 2, 3, 4 }, _subject);
        }

        [Test]
        public void SwapFirstTwoElementsTest()
        {
            _subject.SwapElements(0, 1);
            Assert.AreEqual(new List<int> { 1, 0, 2, 3, 4 }, _subject);
        }

        [Test]
        public void SwapFirstAndLastElementsTest()
        {
            _subject.SwapElements(0, 4);
            Assert.AreEqual(new List<int> { 4, 1, 2, 3, 0 }, _subject);
        }

        [Test]
        public void SwapLastTwoElementsTest()
        {
            _subject.SwapElements(3, 4);
            Assert.AreEqual(new List<int> { 0, 1, 2, 4, 3 }, _subject);
        }

        [Test]
        public void SwapElementsInMiddleTest()
        {
            _subject.SwapElements(1, 3);
            Assert.AreEqual(new List<int> { 0, 3, 2, 1, 4 }, _subject);
        }
    }
}