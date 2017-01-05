using System;
using System.Collections.Generic;

namespace ShuffledNumberGenerator
{
    public static class ListExtensions
    {
        /// <summary>
        /// Swaps two elements within the given list.  Swap is performed
        /// in-place (mutates the list).
        /// </summary>
        /// <typeparam name="T">The type of element the list contains</typeparam>
        /// <param name="list">The "this" argument of this extension method</param>
        /// <param name="firstIndex">The index of the first item to swap</param>
        /// <param name="secondIndex">The index of the second item to swap</param>
        /// <exception cref="ArgumentOutOfRangeException">If either the first or second index are
        /// negative, or greater than or equal to the number of elements in the list</exception>
        public static void SwapElements<T>(this List<T> list,  int firstIndex, int secondIndex)
        {
            // Check for invalid arguments
            if (firstIndex < 0 || firstIndex >= list.Count)
                throw new ArgumentOutOfRangeException(nameof(firstIndex),
                    "cannot swap element at index " + firstIndex + " in list of size " + list.Count);

            if (secondIndex < 0 || secondIndex >= list.Count)
                throw new ArgumentOutOfRangeException(nameof(secondIndex),
                    "cannot swap element at index " + secondIndex + " in list of size " + list.Count);

            // Return early if indeces are the same
            if (firstIndex == secondIndex)
                return; 

            // Swap the elements
            var temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }
}
