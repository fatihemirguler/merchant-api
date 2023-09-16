using System.Text.Json;

namespace MerchantAPI.Models.Loggers;

public class LoggingDetails
{
    public string Timestamp { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public LoggingDetails(int statusCode, string message, string path)
    {
        Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
        StatusCode = statusCode;
        Message = message;
        Path = path;
    }
}