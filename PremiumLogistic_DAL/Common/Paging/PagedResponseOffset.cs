namespace PremiumLogistic_DAL.Common.Paging;

public class PagedResponseOffset<T>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalRecords { get; init; }
    public int TotalPages { get; init; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public List<T> Data { get; init; }

    public PagedResponseOffset(List<T> data, int pageNumber, int pageSize, int totalRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
        HasPreviousPage = pageNumber > 1;
        HasNextPage = pageNumber < TotalPages;
    }
}
