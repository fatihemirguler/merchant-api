using MongoDB.Bson.Serialization.Attributes;

namespace MerchantAPI.Models;

public class Merchant
{
    
    public Merchant(string name, int cityCode, string city, string neighborhood,DateTime createdAt,DateTime updatedAt)
    {
        Id = Guid.NewGuid().ToString(); 
        Name = name;
        var address = new Address(cityCode,city,neighborhood);
        Address = address;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    [BsonElement("_id")]
    public string Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("address")]
    public Address Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class Address
{
    public Address(int cityCode, string city, string neighborhood)
    {
        CityCode = cityCode;
        City = city;
        Neighborhood = neighborhood;
    }

    [BsonElement("city_code")]
    public int CityCode { get; set; }
    
    [BsonElement("city")]
    public string City { get; set; }
    
    [BsonElement("neighborhood")]
    public string Neighborhood { get; set; }
}