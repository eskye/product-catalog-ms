using System;
namespace Catalog.Shared.Core
{
    public class PaginationFilter
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }

    public class BaseQueryFilter : PaginationFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }


    public class BasePageFilter
    {
        public BasePageFilter(int pageSize = 10, int page = 1)
        {
            Page = page <= 0 ? 1 : page;
            PageSize = pageSize <= 0 ? 20 : pageSize;
        } 
        public int PageSize { get; }
        public int Page { get; }
    }
}

