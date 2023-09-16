namespace MerchantAPI.V1.Models.RequestModels;

public class MerchantCreateRequestModel
{
    public MerchantCreateRequestModel(string name, int cityCode, string city, string neighborhood)
    {
        Name = name;
        CityCode = cityCode;
        City = city;
        Neighborhood = neighborhood;
    }

    public string Name { get; set; }
    public int CityCode { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
}