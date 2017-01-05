using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffledNumberGenerator
{
    public static class ListExtensions
    {
        public static void SwapElements<T>(this List<T> list,  int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= list.Count)
                throw new ArgumentOutOfRangeException(nameof(firstIndex),
                    "cannot swap element at index " + firstIndex + " in list of size " + list.Count);

            if (secondIndex < 0 || secondIndex >= list.Count)
                throw new ArgumentOutOfRangeException(nameof(secondIndex),
                    "cannot swap element at index " + secondIndex + " in list of size " + list.Count);

            if (firstIndex == secondIndex)
                return; // Return early, meaningless to swap an element with its self

            var temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }
}
