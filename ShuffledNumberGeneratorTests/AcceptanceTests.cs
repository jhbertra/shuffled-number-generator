using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using Moq;

namespace ShuffledNumberGeneratorTests
{
    [TestFixture]
    public class AcceptanceTests
    {
        [Test]
        public void ResultsContainAllIntegersFromOneToTenThousand()
        {
            var numbersLeftToCheck = Enumerable.Range(1, 10000).ToList();
            var textWriterMock = new Mock<TextWriter>();
            textWriterMock.Setup(writer => writer.WriteLine(It.IsAny<int>()))
                .Callback((int number) => numbersLeftToCheck.Remove(number));

            ShuffledNumberGenerator.Application.RunProgramWithOutput(textWriterMock.Object);

            Assert.IsEmpty(numbersLeftToCheck);
        }

        [Test]
        public void ResultsContainOnlyUniqueIntegers()
        {
            var numbersLeftToCheck = Enumerable.Range(1, 10000).ToList();
            var textWriterMock = new Mock<TextWriter>();
            var resultsAreUnique = true;
            textWriterMock.Setup(writer => writer.WriteLine(It.IsAny<int>()))
                .Callback((int number) => resultsAreUnique = resultsAreUnique && numbersLeftToCheck.Remove(number));

            ShuffledNumberGenerator.Application.RunProgramWithOutput(textWriterMock.Object);

            Assert.IsTrue(resultsAreUnique);
        }

        [Test]
        public void ResultsAreDifferentBetweenMultipleRuns()
        {
            var resultsForEachRun = new List<List<int>> {new List<int>(10000), new List<int>(10000), new List<int>(10000)};
            var currentRun = 0;
            var textWriterMock = new Mock<TextWriter>();
            textWriterMock.Setup(writer => writer.WriteLine(It.IsAny<int>()))
                .Callback((int number) => resultsForEachRun[currentRun].Add(number));

            //Run the program three times to obtain three different results
            while (currentRun < 3)
            {
                ShuffledNumberGenerator.Application.RunProgramWithOutput(textWriterMock.Object);
                ++currentRun;
                Thread.Sleep(100); // suspend execution between runs, to ensure a different random seed is used each run.
            }
            
            //Check the results are not all the same.
            Assert.IsFalse(resultsForEachRun[0].Equals(resultsForEachRun[1]) && resultsForEachRun[1].Equals(resultsForEachRun[2]));

            /*
             * Technically, it is possible for three consecutive runs to each produce the same result.
             * However, assuming that .NET's Math.Random has an unbiased distribution given different
             * seeds, the probability of this occuring is 1:(10000!)^3, or roughly 1:2.3058102e+106978, which
             * is so unlikely that this case can practically be treated as the program not working properly.
             */
        }
    }
}