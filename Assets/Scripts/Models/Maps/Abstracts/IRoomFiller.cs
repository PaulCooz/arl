namespace Models.Maps.Abstracts
{
    public interface IRoomFiller
    {
        void Fill(in Room room, in bool isStart, in bool isExit, in System.Random random);
    }
}