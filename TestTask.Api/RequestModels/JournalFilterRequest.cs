namespace TestTask.Api.RequestModels;

public class JournalFilterRequest
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string Search { get; set; }
    
}