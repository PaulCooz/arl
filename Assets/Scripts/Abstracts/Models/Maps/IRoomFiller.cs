namespace Abstracts.Models.Maps
{
    public interface IRoomFiller
    {
        void Fill(in Room room, in bool isFirst);
    }
}