using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Common.Interpreters
{
    public readonly struct Value
    {
        public static readonly Value Null = new("0");

        [JsonProperty("value")]
        internal readonly string ScriptValue;

        [JsonIgnore]
        public string StringValue
        {
            get
            {
                if (ScriptValue.Length > 0 && Tools.IsQuote(ScriptValue[0]))
                {
                    return ScriptValue.Substring(1, ScriptValue.Length - 2);
                }
                else
                {
                    return ScriptValue;
                }
            }
        }

        [JsonIgnore]
        public int IntValue => (int) DoubleValue;

        [JsonIgnore]
        public double DoubleValue => Convert.ToDouble(ScriptValue, Core.NumberFormat);

        [JsonIgnore]
        public IReadOnlyList<float> ArrFloatValue =>
            ScriptValue.ToValues().Select(value => (float) value.DoubleValue).ToArray();

        [JsonIgnore]
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