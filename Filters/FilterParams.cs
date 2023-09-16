namespace MerchantAPI.Filters;

public class FilterParams
{
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
    public string? City { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
    public string? SearchTerm { get; set; }
    public string? Search { get; set; }
}