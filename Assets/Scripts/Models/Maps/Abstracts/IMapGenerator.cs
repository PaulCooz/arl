using System.Collections.Generic;

namespace Models.Maps.Abstracts
{
    public interface IMapGenerator
    {
        Map GetNextMap(in List<Entities>[,] array, in MapData mapData, in IRoomFiller roomFiller);
    }
}