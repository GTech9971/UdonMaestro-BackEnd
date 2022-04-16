using Microsoft.EntityFrameworkCore;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// ページネーション用クラス
    /// </summary>
    public class PaginationResult<T> {

        public List<T> Data { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPage { get; private set; }

        private PaginationResult(List<T> data,
            int count, int pageIndex, int pageSize) {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        }

        public static async Task<PaginationResult<T>> CreateAsync(IQueryable<T> source,
            int pageIndex, int pageSize) {

            var count = await source.CountAsync();
            source = source.Skip(pageIndex * pageSize).Take(pageSize);
            var data = await source.ToListAsync();
            return new PaginationResult<T>(data, count, pageIndex, pageSize);
        }

        public bool HasPrevisouPage {
            get {
                return (PageIndex > 0);
            }
        }

        public bool HasNextPage {
            get {
                return ((PageIndex + 1) < TotalPage);
            }
        }


    }
}
