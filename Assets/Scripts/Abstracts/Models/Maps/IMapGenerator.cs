using System.Collections.Generic;

namespace Abstracts.Models.Maps
{
    public interface IMapGenerator
    {
        Map GetNextMap(in List<Entities>[,] array, in MapData mapData, in IRoomFiller roomFiller);
    }
}