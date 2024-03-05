using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestTask.Application.Common.Interfaces;
using TestTask.Domain.Entities;

namespace TestTask.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    private readonly IJournalRepository _journalRepository;

    public ErrorHandlingFilterAttribute(IJournalRepository journalRepository)
    {
        _journalRepository = journalRepository;
    }
    
    public override async  Task OnExceptionAsync(ExceptionContext context)
    {
        var exceptionContext = new
        {
            Type = context.Exception.GetType().Name,
            Id = Guid.NewGuid(),
            Data = new
            {
                Message = context.Exception.Message
            }
        };

        using var reader = new StreamReader(context.HttpContext.Request.Body);
        var body = await reader.ReadToEndAsync();
        
        var log = new JournalLog
        {
            EventId = Guid.NewGuid(),
            Type = context.Exception.GetType().Name,
            Message = context.Exception.Message,
            StackTrace = context.Exception.StackTrace,
            Request = body,
            QueryParameters = JsonSerializer.Serialize(context.HttpContext.Request.Query),
            TimeStamp = DateTime.Now
        };

        await _journalRepository.AddLog(log);
        

        context.Result = new ObjectResult(exceptionContext);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        context.ExceptionHandled = true;
    }

   
}