namespace MerchantAPI.Models.Errors;

public class BadRequest:ErrorDetails
{
    public BadRequest(string message)
    {
        StatusCode = 400;
        Message = message;
    }
}
public class EmptyMerchantName : BadRequest
{
    public EmptyMerchantName(string name):base(message:$"Invalid Name: {name}") {}
}
public class EmptyCityCode : BadRequest
{
    public EmptyCityCode():base(message:"Empty CityCode.") {}
}
public class NonpositiveCityCode : BadRequest
{
    public NonpositiveCityCode(int cityCode):base(message:$"Nonpositive CityCode: {cityCode}") {}
}
public class EmptyCityName : BadRequest
{ 
    public EmptyCityName(string cityname):base(message:$"Invalid City.{cityname}") {}
}
public class EmptyNeighborhood: BadRequest
{ 
    public EmptyNeighborhood(string neighborhood):base(message:$"Invalid City.{neighborhood}") {}
}
public class ImproperSearch: BadRequest
{ 
    public ImproperSearch(string? searchTerm, string? search):base(message:$"Improper Search. (SearchTerm:{searchTerm}, Search:{search})") {}
}
