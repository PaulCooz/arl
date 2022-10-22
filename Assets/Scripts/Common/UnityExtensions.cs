using UnityEngine;

namespace Common
{
    public static class UnityExtensions
    {
        public static Vector2 Rotate(this Vector2 v, float angleDeg)
        {
            var angle = angleDeg * Mathf.Deg2Rad;
            return new Vector2
            (
                Mathf.Cos(angle) * v.x + Mathf.Sin(angle) * v.y,
                Mathf.Cos(angle) * v.y - Mathf.Sin(angle) * v.x
            );
        }

        public static bool NotZero(this Vector2 v)
        {
            return Mathf.Abs(v.x) + Mathf.Abs(v.y) > float.Epsilon;
        }
    }
}