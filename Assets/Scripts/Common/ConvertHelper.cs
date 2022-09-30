using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class ConvertHelper
    {
        public static Color32 ArrayToColor(IReadOnlyList<byte> bytes)
        {
            var result = new Color32(255, 255, 255, 255);

            if (bytes.Count > 0) result.r = bytes[0];
            if (bytes.Count > 1) result.g = bytes[1];
            if (bytes.Count > 2) result.b = bytes[2];
            if (bytes.Count > 3) result.a = bytes[3];

            return result;
        }
    }
}