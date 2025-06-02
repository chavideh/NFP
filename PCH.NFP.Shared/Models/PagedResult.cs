namespace PCH.NFP.Shared.Models;


public static class PaginationExtensions
{
    public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> query, int page, int pageSize)
    {
        var totalItems = query.Count();
        var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<T>(items, totalItems, page, pageSize);
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; }
    public int TotalItems { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages => (int)System.Math.Ceiling((double)TotalItems / PageSize);

    public PagedResult(List<T> items, int totalItems, int page, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        Page = page;
        PageSize = pageSize;
    }
}



