namespace PremiumLogistic_BAL.Dtos;

public record PagedResponseOffsetDto<T>(
    int PageNumber,
    int PageSize,
    int TotalPages,
    int TotalRecords,
    bool HasPreviousPage,
    bool HasNextPage,
    List<T> Data
);
