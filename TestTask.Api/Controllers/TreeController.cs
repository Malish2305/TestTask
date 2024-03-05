using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTask.Api.ResponseModels;
using TestTask.Application.Trees.Commands;
using TestTask.Application.Trees.Queries;

namespace TestTask.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TreeController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TreeController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("get")]
    public async Task<IActionResult> GetTree(string treeName)
    {
        var query = new GetTreeQuery(treeName);
        var result = await _sender.Send(query);
        var response = _mapper.Map<TreeNodeResponseModel>(result);
        return Ok(response);
    }
    
    [HttpPost("node/create")]
    public async Task<IActionResult> CreateNode(string treeName, int parentNodeId, string nodeName)
    {
        var command = new CreateNodeCommand(treeName, parentNodeId, nodeName);
        await _sender.Send(command);
        return Ok();
    }

    [HttpPost("node/delete")]
    public async Task<IActionResult> DeleteNode(string treeName, int nodeId)
    {
        var command = new DeleteNodeCommand(treeName, nodeId);
        await _sender.Send(command);
        return Ok();
    }
    
    [HttpPost("node/rename")]
    public async Task<IActionResult> RenameNode(string treeName, int nodeId, string newNodeName)
    {

        var command = new RenameNodeCommand(treeName, nodeId, newNodeName);
        await _sender.Send(command);
        return Ok();
    }
    
    
}