using MongoDB.Bson.Serialization.Attributes;

namespace MerchantAPI.Models;

public class UserDatabaseModel
{
    [BsonElement("username")]
    public string Username { get; set; }
    
    [BsonElement("_id")]
    public string Id { get; set; }
    [BsonElement("authToken")]
    public string AuthToken { get; set; }
    
    public UserDatabaseModel(string username, string authToken)
    {
        
        Username = username;
        AuthToken = authToken;
        Id = Guid.NewGuid().ToString();
    }
}