using System;

namespace Common
{
    public static class RandomExtensions
    {
        public static bool NextBool(this Random random)
        {
            return random.Next(0, 2) == 0;
        }

        public static bool Chance(this Random random, int chance)
        {
            return random.Next(0, 100) < chance;
        }
    }
}