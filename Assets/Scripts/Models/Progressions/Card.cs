using System;
using UnityEngine;

namespace Models.Progressions
{
    [Serializable]
    public class Card
    {
        public string description;

        [TextArea]
        public string command;
    }
}