using System;
using Newtonsoft.Json;

namespace Models.Maps.Abstracts
{
    [Serializable]
    public struct RoomData
    {
        [JsonProperty("min_height")]
        public int minHeight;
        [JsonProperty("min_width")]
        public int minWidth;
        [JsonProperty("max_height")]
        public int maxHeight;
        [JsonProperty("max_width")]
        public int maxWidth;
    }
}