using X.PagedList;

namespace Chat.Application.Helper.Extentions
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var total = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            this.AddRange(items);

            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalCount = total;
            this.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }

        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }

        public bool HasNextPage
        {
            get { return (PageIndex < TotalPages); }
        }

        public int PageCount => throw new NotImplementedException();

        public int TotalItemCount => throw new NotImplementedException();

        public int PageNumber => throw new NotImplementedException();

        public bool IsFirstPage => throw new NotImplementedException();

        public bool IsLastPage => throw new NotImplementedException();

        public int FirstItemOnPage => throw new NotImplementedException();

        public int LastItemOnPage => throw new NotImplementedException();

        public static PagedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
        }

        public PagedListMetaData GetMetaData()
        {
            throw new NotImplementedException();
        }
    }
}
