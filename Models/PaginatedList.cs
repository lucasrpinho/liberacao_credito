using Microsoft.EntityFrameworkCore;

namespace liberacao_credito.Models
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(Count / (double)pageSize);
            this.AddRange(items);
        }

        public int PageIndex { get; private set; }
        public int TotalPages { get; set; }       
        public bool PreviousPage
        {
            get { return (PageIndex > 1); }
        }
        public bool NextPage
        {
            get { return (PageIndex < TotalPages); }
        }
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

    }
}
