using System;
using Newtonsoft.Json;

namespace Models.Maps.Abstracts
{
    [Serializable]
    public struct MapData
    {
        [JsonProperty("height")]
        public int height;
        [JsonProperty("width")]
        public int width;

        [JsonProperty("room")]
        public RoomData room;

        public int seed;
    }
}