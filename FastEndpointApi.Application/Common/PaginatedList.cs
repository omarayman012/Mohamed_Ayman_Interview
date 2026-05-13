using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace FastEndpointApi.Application.Common
{
    public class PaginatedList<T>(List<T> items, int pageNumber, int count, int pageSize)
    {
        public List<T> Items { get; set; } = items;
        public int PageNumber { get; set; } = pageNumber;
        public int TotalCount { get; set; } = count;
        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static Task<PaginatedList<T>> CreateAsync(
      IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(
                new PaginatedList<T>(items, pageNumber, count, pageSize)
            );
        }
    }
}
