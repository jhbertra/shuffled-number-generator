using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShuffledNumberGenerator
{
    public class Program
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
            var list = CreateList();
            ShuffleList(list, new Random());
            WriteListToOutput(list, output);
        }

        /// <summary>
        /// Creates the set of all integers from 1 to 10000, sorted in ascending 
        /// order.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> containing the set described above.
        /// </returns>
        public static List<int> CreateList()
        {
            return Enumerable.Range(1, 10000).ToList();
        }

        /// <summary>
        /// Shuffles the list randomly using the Fisher-Yates shuffle
        /// algorithm. Performs the shuffle in-place, in O(n) time
        /// complexity.
        /// </summary>
        /// <param name="list">The list to shuffle.</param>
        /// <param name="random">
        /// The pseudo-random number generator used to pick elements to swap.
        /// </param>
        public static void ShuffleList(List<int> list, Random random)
        {
            // While there are remaining elements to shuffle
            for (var remaining = list.Count; remaining > 0; --remaining)
            {
                // randomly pick an unshuffled element
                var randomUnshuffledIndex = (int)(random.NextDouble() * remaining);

                // and swap it with the last unshuffled element
                var lastUnshuffledIndex = remaining - 1;
                list.SwapElements(randomUnshuffledIndex, lastUnshuffledIndex);
            }
        }

        /// <summary>
        /// Writes the provided enumerable sequence, in order, to the
        /// given output.
        /// </summary>
        /// <param name="list">The enumerable sequence to write</param>
        /// <param name="output">The <see cref="TextWriter"/> to write the list to.</param>
        private static void WriteListToOutput(IEnumerable<int> list, TextWriter output)
        {
            foreach (var element in list)
            {
                output.WriteLine(element);
            }
        }
    }
}
