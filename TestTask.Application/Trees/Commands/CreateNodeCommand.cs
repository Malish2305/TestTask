using MediatR;
using TestTask.Application.Common.Interfaces;
using TestTask.Domain.Entities;

namespace TestTask.Application.Trees.Commands;

public record CreateNodeCommand(string TreeName, int ParentModeId, string NodeName) : IRequest;

public class CreateNodeCommandHandler : IRequestHandler<CreateNodeCommand>
{
    private readonly ITreeRepository _treeRepository;

    public CreateNodeCommandHandler(ITreeRepository treeRepository)
    {
        _treeRepository = treeRepository;
    }
    
    public async Task Handle(CreateNodeCommand request, CancellationToken cancellationToken)
    {
        var node = new TreeNode()
        {
            Name = request.NodeName
        };

        await _treeRepository.CreateNode(node, request.TreeName, request.ParentModeId);
    }
}
