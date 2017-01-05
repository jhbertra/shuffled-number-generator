using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffledNumberGenerator
{
    public class SwappableElementList<TItem> : List<TItem>
    {
        public SwappableElementList(int capacity) : base(capacity)
        {
        }

        public void SwapElements(int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= Count)
                throw new ArgumentOutOfRangeException(nameof(firstIndex),
                    "cannot swap element at index " + firstIndex + " in list of size " + Count);

            if (secondIndex < 0 || secondIndex >= Count)
                throw new ArgumentOutOfRangeException(nameof(secondIndex),
                    "cannot swap element at index " + secondIndex + " in list of size " + Count);

            if (firstIndex == secondIndex)
                return; // Return early, meaningless to swap an element with its self

            var temp = this[firstIndex];
            this[firstIndex] = this[secondIndex];
            this[secondIndex] = temp;
        }
    }
}
