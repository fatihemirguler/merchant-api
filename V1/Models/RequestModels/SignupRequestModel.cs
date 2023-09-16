namespace MerchantAPI.V1.Models.RequestModels;

public class SignupRequestModel
{
    public SignupRequestModel(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}