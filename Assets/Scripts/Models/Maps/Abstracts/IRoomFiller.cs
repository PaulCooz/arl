using System.Collections.Generic;

namespace Models.Maps.Abstracts
{
    public interface IRoomFiller
    {
        void Setup(in List<Entities>[,] array);

        void FillStart(in Room room, System.Random random);
        void FillCommon(in Room room, System.Random random);
        void FillExit(in Room room, System.Random random);
    }
}