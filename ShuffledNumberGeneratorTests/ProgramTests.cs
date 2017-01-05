using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using Moq;

namespace ShuffledNumberGenerator.Tests
{
    [TestFixture]
    public class ProgramTests
    {

        [Test]
        public void CreateList_ExpectedSequenceReturned()
        {
            Assert.AreEqual(Enumerable.Range(1, 10000), Program.CreateList());
        }

        [Test]
        public void ShuffleList_RngAlwaysReturnsZero_FirstElementIsMovedToEnd()
        {
            var list = Enumerable.Range(1, 5).ToList();
            var rngMock = new Mock<Random>();
            rngMock.Setup(random => random.NextDouble()).Returns(0).Verifiable();

            Program.ShuffleList(list, rngMock.Object);

            Assert.AreEqual(new List<int> { 2, 3, 4, 5, 1 }, list);
            rngMock.Verify();
        }

        [Test]
        public void ShuffleList_RngAlwaysReturnsAlmostOne_ListOrderIsUnchanged()
        {
            var list = Enumerable.Range(1, 5).ToList();
            var rngMock = new Mock<Random>();
            rngMock.Setup(random => random.NextDouble()).Returns(0.9999).Verifiable();

            Program.ShuffleList(list, rngMock.Object);

            Assert.AreEqual(new List<int> { 1, 2, 3, 4, 5 }, list);
        }

        [Test]
        public void ShuffleList_RngReturnsSpecificSequence_ListOrderIsShuffledAsExpected()
        {
            // the "randomly" selected indecies each iteration, in order will be: 1, 0, 1, 1, 0 
            var rngSequence = new List<double> { 0.2, 0, 0.334, 0.999, 0 };
            var currentSequencePosition = 0;

            var list = Enumerable.Range(1, 5).ToList();
            var rngMock = new Mock<Random>();
            rngMock.Setup(random => random.NextDouble()).Returns(() => rngSequence[currentSequencePosition++]).Verifiable();

            Program.ShuffleList(list, rngMock.Object);

            Assert.AreEqual(new List<int> { 4, 3, 5, 1, 2 }, list);
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
             * [4, 3, 5, 1, 2] swap with element 0
             *  ^ 
             */
        }

        [Test]
        public void RunProgramWithOutput_ResultContainsAllIntegersFromOneToTenThousand()
        {
            var numbersLeftToCheck = Enumerable.Range(1, 10000).ToList();
            var textWriterMock = new Mock<TextWriter>();
            textWriterMock.Setup(writer => writer.WriteLine(It.IsAny<int>()))
                .Callback((int number) => numbersLeftToCheck.Remove(number));

            Program.RunProgramWithOutput(textWriterMock.Object);

            Assert.IsEmpty(numbersLeftToCheck);
        }

        [Test]
        public void RunProgramWithOutput_ResultContainsOnlyUniqueIntegers()
        {
            var numbersLeftToCheck = Enumerable.Range(1, 10000).ToList();
            var textWriterMock = new Mock<TextWriter>();
            var resultsAreUnique = true;
            textWriterMock.Setup(writer => writer.WriteLine(It.IsAny<int>()))
                .Callback((int number) => resultsAreUnique = resultsAreUnique && numbersLeftToCheck.Remove(number));

            Program.RunProgramWithOutput(textWriterMock.Object);

            Assert.IsTrue(resultsAreUnique);
        }

        [Test]
        public void RunProgramWithOutput_ResultsAreDifferentBetweenMultipleRuns()
        {
            var resultsForEachRun = new List<List<int>> {new List<int>(10000), new List<int>(10000), new List<int>(10000)};
            var currentRun = 0;
            var textWriterMock = new Mock<TextWriter>();
            textWriterMock.Setup(writer => writer.WriteLine(It.IsAny<int>()))
                .Callback((int number) => resultsForEachRun[currentRun].Add(number));

            //Run the program three times to obtain three different results
            while (currentRun < 3)
            {
                Program.RunProgramWithOutput(textWriterMock.Object);
                ++currentRun;
                Thread.Sleep(100); // suspend execution between runs, to ensure a different random seed is used each run.
            }
            
            //Check the results are not all the same.
            Assert.IsFalse(resultsForEachRun[0].Equals(resultsForEachRun[1]) && resultsForEachRun[1].Equals(resultsForEachRun[2]));

            /*
             * Technically, it is possible for three consecutive runs to each produce the same result.
             * However, assuming that .NET's Math.Random is unbiased in its distribution given different
             * seeds, the probability of this occuring is 1:(10000!)^3 = roughly 1:2.3058102e+106978, which
             * is so unlikely that this case can practically be treated as the program not working properly.
             */
        }
    }
}