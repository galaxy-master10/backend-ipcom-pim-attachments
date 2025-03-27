namespace dotnet_ipcom_pim_share.Common.Utilities;

public class PaginatedResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public int? ExpiringWithin7Days { get; set; }
    public int? ExpiringWithin30Days { get; set; }
    public List<T> Data { get; set; }

    public PaginatedResponse(List<T> data, int pageNumber, int pageSize, int totalRecords,  int? expiringWithin7Days = null, int? expiringWithin30Days = null)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        ExpiringWithin7Days = expiringWithin7Days;
        ExpiringWithin30Days = expiringWithin30Days;
    }
}