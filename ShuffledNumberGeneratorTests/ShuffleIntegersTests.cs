using System;
using NUnit.Framework;
using Moq;

namespace ShuffledNumberGeneratorTests
{
    [TestFixture]
    public class ShuffleIntegersTests
    {
        private int[] _integers;
        private Mock<Random> _rngMock;

        [SetUp]
        public void SetUp()
        {
            _integers = new[] {1, 2, 3, 4, 5};
            _rngMock = new Mock<Random>();
        }

        [TearDown]
        public void TearDown()
        {
            _rngMock.Verify();
        }

        [Test]
        public void RngAlwaysReturnsZero_FirstElementIsMovedToEnd()
        {
            _rngMock.Setup(random => random.NextDouble()).Returns(0).Verifiable();

            ShuffledNumberGenerator.Application.ShuffleIntegers(_integers, _rngMock.Object);

            Assert.AreEqual(new[] { 2, 3, 4, 5, 1 }, _integers);
        }

        [Test]
        public void RngAlwaysReturnsAlmostOne_OrderIsUnchanged()
        {
            _rngMock.Setup(random => random.NextDouble()).Returns(0.9999).Verifiable();

            ShuffledNumberGenerator.Application.ShuffleIntegers(_integers, _rngMock.Object);

            Assert.AreEqual(new[] { 1, 2, 3, 4, 5 }, _integers);
        }

        [Test]
        public void RngReturnsSpecificSequence_OrderIsShuffledAsExpected()
        {
            // the "randomly" selected indices each iteration, in order will be: 1, 0, 1, 1, 0 
            var rngSequence = new[] { 0.2, 0, 0.334, 0.999, 0 };
            var currentSequencePosition = 0;
            _rngMock.Setup(random => random.NextDouble()).Returns(() => rngSequence[currentSequencePosition++]).Verifiable();

            ShuffledNumberGenerator.Application.ShuffleIntegers(_integers, _rngMock.Object);

            Assert.AreEqual(new[] { 4, 3, 5, 1, 2 }, _integers);
            /*
             * Computation of expected order:
             * 
             * [1, 2, 3, 4, 5] swap with element 1
             *              ^
             * [1, 5, 3, 4, 2] swap with element 0
             *           ^
             * [4, 5, 3, 1, 2] swap with element 1
             *        ^ 
             * [4, 3, 5, 1, 2] swap with element 1
             *     ^
             */
        }

        [Test]
        public void SingleElementArray_RngNeverCalled()
        {
            ShuffledNumberGenerator.Application.ShuffleIntegers(new[] {0}, _rngMock.Object);
            _rngMock.Verify(rng => rng.NextDouble(), Times.Never);
        }

        [Test]
        public void ZeroElementArray_RngNeverCalled()
        {
            ShuffledNumberGenerator.Application.ShuffleIntegers(new int[0], _rngMock.Object);
            _rngMock.Verify(rng => rng.NextDouble(), Times.Never);
        }
    }
}