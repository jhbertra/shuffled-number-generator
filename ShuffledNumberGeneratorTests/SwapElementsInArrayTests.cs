using NUnit.Framework;
using ShuffledNumberGenerator;

namespace ShuffledNumberGeneratorTests
{
    [TestFixture]
    public class SwapElementsInArrayTests
    {
        private int[] _array;

        [SetUp]
        public void SetUp()
        {
            _array = new[] {0, 1, 2, 3, 4};
        }
        
        [Test]
        public void SwapSameElementTest()
        {
            Application.SwapElementsInArray(_array, 0, 0);
            Assert.AreEqual(new[] { 0, 1, 2, 3, 4 }, _array);
        }

        [Test]
        public void SwapFirstTwoElementsTest()
        {
            Application.SwapElementsInArray(_array, 0, 1);
            Assert.AreEqual(new[] { 1, 0, 2, 3, 4 }, _array);
        }

        [Test]
        public void SwapFirstAndLastElementsTest()
        {
            Application.SwapElementsInArray(_array, 0, 4);
            Assert.AreEqual(new[] { 4, 1, 2, 3, 0 }, _array);
        }

        [Test]
        public void SwapLastTwoElementsTest()
        {
            Application.SwapElementsInArray(_array, 3, 4);
            Assert.AreEqual(new[] { 0, 1, 2, 4, 3 }, _array);
        }

        [Test]
        public void SwapElementsInMiddleTest()
        {
            Application.SwapElementsInArray(_array, 1, 3);
            Assert.AreEqual(new[] { 0, 3, 2, 1, 4 }, _array);
        }
    }
}