namespace MerchantAPI.V1.Models.RequestModels;

public class LoginRequestModel
{
    public LoginRequestModel(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}