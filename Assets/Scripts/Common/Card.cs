using System;
using UnityEngine;

namespace Common
{
    [Serializable]
    public struct Card
    {
        public string description;

        [TextArea]
        public string command;
    }
}