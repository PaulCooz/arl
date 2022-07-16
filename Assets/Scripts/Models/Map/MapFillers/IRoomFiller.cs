namespace Models.Map.MapFillers
{
    public interface IRoomFiller
    {
        void Fill(in ushort[,] map, in Room room);
        void Conclude(in ushort[,] map);
    }
}
