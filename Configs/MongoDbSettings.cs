namespace MerchantAPI.Configs;

public class MongoDbSettings
{
    public string? ConnectionString { get; set; }
    public string? DbName { get; set; }
    public string? MerchantCollectionName { get; set; }
    public string? UserCollectionName { get; set; }
}