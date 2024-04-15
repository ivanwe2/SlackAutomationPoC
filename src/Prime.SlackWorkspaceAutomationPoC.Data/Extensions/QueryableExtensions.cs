using Microsoft.EntityFrameworkCore;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> PaginateAsync<T>(this IQueryable<T> collection, int pageNumber, int pageSize)
        {
            var count = await collection.CountAsync();
            var skip = (pageNumber - 1) * pageSize;
            if (count == 0 || count < skip)
            {
                return PaginatedResult<T>.EmptyResult(10,1);
            }

            var result = await collection.Skip(skip).Take(pageSize).ToListAsync();

            return new PaginatedResult<T>(result, count, 1, 10);
        }
    }
}
