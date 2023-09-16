using MerchantAPI.Models;

namespace MerchantAPI.V1.Models.ResponseModels;

public class MerchantResponseModel
{
    public MerchantResponseModel(Merchant merchant)
    {
        Name = merchant.Name;
        var address = new MerchantAddress(merchant.Address.CityCode
            ,merchant.Address.City,merchant.Address.Neighborhood);
        MerchantAddress = address;
        CreatedAt = merchant.CreatedAt;
        UpdatedAt = merchant.UpdatedAt;
    }
    public string Name { get; set; }
    public MerchantAddress MerchantAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class MerchantAddress
{
    public MerchantAddress(int cityCode, string city, string neighborhood)
    {
        CityCode = cityCode;
        City = city;
        Neighborhood = neighborhood;
    }
    public int CityCode { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
}