namespace Catalog.Shared.AppResponse
{
    public class PaginatedResponse<T>
	{
        public PaginatedResponse(IReadOnlyList<T> items, PaginationMetaData meta)
        {
            Items = items;
            Meta = meta;
        }
        public IReadOnlyList<T> Items { get; }
        public PaginationMetaData Meta { get; }
    }

    public class PaginationMetaData
    {
        public PaginationMetaData(int page, int perPage, int total)
        {
            Page = page;
            PerPage = perPage;
            Total = total;
        }

        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}

