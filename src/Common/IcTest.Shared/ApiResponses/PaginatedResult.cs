namespace IcTest.Shared.ApiResponses
{
    /// <summary>
    /// Abstraction for paginated results
    /// </summary>
    /// <typeparam name="T">Model of the data that will paginated</typeparam>
    /// <param name="pageNumber">Number of current page</param>
    /// <param name="pageSize">Size of the page</param>
    /// <param name="count">Number of total records</param>
    /// <param name="data">List of data</param>
    public class PaginatedResult<T>
        (int pageNumber, int pageSize, long count, IEnumerable<T> data)
    {
        public int PageNumber{ get; } = pageNumber;
        public int PageSize { get; } = pageSize;
        public long Count { get; } = count;
        public IEnumerable<T> Data { get; } = data;
    }
}
