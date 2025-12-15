using Microsoft.EntityFrameworkCore;

namespace InventoryV2.Shares;


public class PagedResult<T>
{
    public List<T> Data { get; set; } = new();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}

public static class PaginationExtension
{
    public static async Task<PagedResult<T>> ToPagedListAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        var count = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        bool hasPreviousPage = pageNumber > 1;
        return new PagedResult<T>()
        {
            Data = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = count,
        };
    }
}