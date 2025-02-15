namespace API.RequestHelpers
{
    public class Pagination<T>
    {
        public Pagination(int PageIndex, int PageSize, int Count, IReadOnlyList<T> Data)
        {
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.Count = Count;
            this.Data = Data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

    }
}
