using System.Collections.Generic;

namespace Libs
{
    public static class ExternalMethods
    {
        #region Random

        public static T Rand<T>(this IList<T> collection)
        {
            return collection[UnityEngine.Random.Range(0, collection.Count)];
        }

        public static void RandomShuffle<T>(this IList<T> list, in System.Random random)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var j = random.Next(0, list.Count);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        public static bool Chance(this System.Random random, byte chance)
        {
            return random.Next(0, 100) < chance;
        }

        public static int Next(this System.Random random, MinMaxInt range)
        {
            return random.Next(range.Min, range.Max + 1);
        }

        public static int NextInt(this System.Random random)
        {
            return random.Next(int.MinValue, int.MaxValue);
        }

        #endregion

        #region ReadOnlyList

        public static T Front<T>(this IReadOnlyList<T> readOnlyList)
        {
            return readOnlyList[0];
        }

        public static T Back<T>(this IReadOnlyList<T> readOnlyList)
        {
            return readOnlyList[readOnlyList.Count - 1];
        }

        public static bool IsEmpty<T>(this IReadOnlyCollection<T> readOnlyCollection)
        {
            return readOnlyCollection == null || readOnlyCollection.Count < 1;
        }

        #endregion
    }
}
