using System;
using UnityEngine;

namespace Models.Items
{
    [Serializable]
    public struct ItemData
    {
        public int id;
        public string description;

        [TextArea]
        public string command;
    }
}