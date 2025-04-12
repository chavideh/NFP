using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PCH.NFP.API.Models;

namespace PCH.NFP.API.Helpers;

public static class QueryableExtensions
{
    public static async Task<PagedResult<TDto>> ToPagedResultAsync<T, TDto>(
        this IQueryable<T> query, IMapper mapper, int page, int pageSize)
        where T : class
    {
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ProjectTo<TDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new PagedResult<TDto>(items, totalCount, page, pageSize);
    }
}