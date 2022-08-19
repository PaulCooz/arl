using System;
using System.Collections.Generic;

namespace Common.Arrays
{
    public static class ReadOnlyListExtension
    {
        public static bool Contains<T>(this IEnumerable<T> array, params T[] elements) where T : IComparable<T>
        {
            foreach (var a in array)
            {
                foreach (var element in elements)
                {
                    if (a.CompareTo(element) == 0) return false;
                }
            }

            return true;
        }

        public static bool IsEmpty<T>(this IReadOnlyCollection<T> readOnlyList)
        {
            return readOnlyList == null || readOnlyList.Count <= 0;
        }

        public static T Front<T>(this IReadOnlyList<T> readOnlyList)
        {
            return readOnlyList[0];
        }

        public static T Back<T>(this IReadOnlyList<T> readOnlyList)
        {
            return readOnlyList[^1];
        }

        public static void RandomShuffle<T>(this IList<T> readOnlyList, in Random random)
        {
            for (var i = 0; i < readOnlyList.Count; i++)
            {
                var j = random.Next(0, readOnlyList.Count);

                (readOnlyList[i], readOnlyList[j]) = (readOnlyList[j], readOnlyList[i]);
            }
        }
    }
}