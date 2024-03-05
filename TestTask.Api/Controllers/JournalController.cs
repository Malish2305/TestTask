using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTask.Api.RequestModels;
using TestTask.Application.Trees.Queries;

namespace TestTask.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class JournalController : ControllerBase
{
    private readonly ISender _sender;

    public JournalController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost("getSingle")]
    public async Task<IActionResult> GetSingle(int id)
    {
        var query = new GetJournalQuery(id);
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPost("getRange")]
    public async Task<IActionResult> GetRange(int skip, int take, [FromBody] JournalFilterRequest filter)
    {
        var query = new GetJournalRangeQuery(skip, take, filter.From, filter.To, filter.Search);
        var result = await _sender.Send(query);
        return Ok(result);
    }
}