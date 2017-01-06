using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShuffledNumberGenerator
{
    public class Application
    {
        /// <summary>
        /// Starting point of the program.  Writes results to the standard
        /// output stream.
        /// </summary>
        /// <param name="args">Command line arguments.  Not used.</param>
        public static void Main(string[] args)
        {
            RunProgramWithOutput(Console.Out);
        }

        /// <summary>
        /// Generates a list of 10,000 numbers in random order. Each number in the 
        /// list is unique and is between 1 and 10,000 (inclusive). Writes the
        /// results to the provided output.
        /// </summary>
        /// <param name="output">
        /// The <see cref="TextWriter"/> to which the output will be written
        /// </param>
        public static void RunProgramWithOutput(TextWriter output)
        {
            var integers = Enumerable.Range(1, 10000).ToArray();
            ShuffleIntegers(integers, new Random());
            WriteIntegersToOutput(integers, output);
        }

        /// <summary>
        /// Shuffles an integer array randomly using the improved Fisher-Yates shuffle 
        /// algorithm (Durstenfeld).
        /// </summary>
        /// <param name="integers">The array of integers to shuffle.</param>
        /// <param name="random">
        /// The pseudo-random number generator used to pick elements to swap.
        /// </param>
        public static void ShuffleIntegers(int[] integers, Random random)
        {
            // While there are remaining elements to shuffle
            for (var lastUnshuffledIndex = integers.Length - 1; lastUnshuffledIndex > 0; --lastUnshuffledIndex)
            {
                // randomly pick any unshuffled element, which could be the last unshuffled element
                var randomUnshuffledIndex = (int)(random.NextDouble() * (lastUnshuffledIndex + 1));

                // and swap it with the last unshuffled element
                SwapElementsInArray(integers, randomUnshuffledIndex, lastUnshuffledIndex);
            }
        }

        /// <summary>
        /// Swaps the element at <code>fistIndex</code> with the element at
        /// <code>secondIndex</code> in <code>array</code>.
        /// </summary>
        /// <param name="array">The array whose elements to swap</param>
        /// <param name="firstIndex">The index of the first element to swap</param>
        /// <param name="secondIndex">The index of the second element to swap</param>
        public static void SwapElementsInArray(int[] array, int firstIndex, int secondIndex)
        {
            // Only swap if indices are not the same
            if (firstIndex == secondIndex)
                return;

            // Swap the elements
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        /// <summary>
        /// Writes the provided integer sequence, in order, to the given output.
        /// </summary>
        /// <param name="integers">The integer sequence to write</param>
        /// <param name="output">The <see cref="TextWriter"/> to write the integers to.</param>
        private static void WriteIntegersToOutput(IEnumerable<int> integers, TextWriter output)
        {
            foreach (var integer in integers)
            {
                output.WriteLine(integer);
            }
        }
    }
}
