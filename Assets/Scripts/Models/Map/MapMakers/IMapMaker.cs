using Models.Map.MapFillers;
using UnityEngine;

namespace Models.Map.MapMakers
{
    public interface IMapMaker
    {
        ushort[,] GetMap(in Vector2Int startPos, in IRoomFiller roomFiller, in int seed);
    }
}
