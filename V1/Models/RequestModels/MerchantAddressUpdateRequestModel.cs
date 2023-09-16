namespace MerchantAPI.V1.Models.RequestModels;

public class MerchantAddressUpdateRequestModel
{
    public MerchantAddressUpdateRequestModel(int cityCode, string city, string neighborhood)
    {
        CityCode = cityCode;
        City = city;
        Neighborhood = neighborhood;
    }

    public int CityCode { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
}