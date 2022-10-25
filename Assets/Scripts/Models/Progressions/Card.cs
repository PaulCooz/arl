﻿using System;
using UnityEngine;

namespace Models.Progressions
{
    [Serializable]
    public struct Card
    {
        public string description;

        [TextArea]
        public string command;
    }
}