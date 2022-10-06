using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Interpreters
{
    public readonly struct Value
    {
        public static readonly Value Null = new("0");

        internal readonly string ScriptValue;

        public string StringValue => ScriptValue.Substring(1, ScriptValue.Length - 2);
        public int IntValue => Convert.ToInt32(ScriptValue);
        public double DoubleValue => Convert.ToDouble(ScriptValue, Core.NumberFormat);

        public IReadOnlyList<float> ArrFloatValue =>
            ScriptValue.ToValues().Select(value => (float) value.DoubleValue).ToArray();

        public Vector3 Vector3Value
        {
            get
            {
                var res = new Vector3();
                var arr = ArrFloatValue;

                if (arr.Count > 0) res.x = arr[0];
                if (arr.Count > 1) res.y = arr[1];
                if (arr.Count > 2) res.z = arr[2];

                return res;
            }
        }

        public Value(string scriptValue)
        {
            ScriptValue = scriptValue;
        }
    }
}