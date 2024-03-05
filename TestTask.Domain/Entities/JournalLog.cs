namespace TestTask.Domain.Entities;

public class JournalLog
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public Guid EventId { get; set; }
    public DateTime TimeStamp { get; set; }
    public string StackTrace { get; set; }
    public string Request { get; set; }
    public string QueryParameters { get; set; }
}